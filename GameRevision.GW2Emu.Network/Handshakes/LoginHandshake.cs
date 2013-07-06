using System;
using GameRevision.GW2Emu.Core.Serializers;
using GameRevision.GW2Emu.Core.Cryptography;

namespace GameRevision.GW2Emu.Network.Handshakes
{
    public class LoginHandshake
    {
        public event Action HandshakeDone;
        public INetworkSession NetworkSession { get; private set; }
        public uint Version { get; private set; }
        public byte[] RC4Key { get; private set; }

        public LoginHandshake(INetworkSession networkSession)
        {
            this.NetworkSession = networkSession;
            this.NetworkSession.DataReceived += OnDataReceived;
        }

        public void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            Deserializer deserializer = new Deserializer(e.Buffer);

            while (deserializer.BaseStream.Position < deserializer.BaseStream.Length)
            {
                byte header = deserializer.ReadByte();
                byte length = deserializer.ReadByte();

                switch (length)
                {
                    case 0x04: // Version
                        deserializer.BaseStream.Position += 2;
                        this.Version = deserializer.ReadUInt32();
                        deserializer.BaseStream.Position += 8;
                        break;
                    case 0x42: // PublicKey
                        byte[] publicKey = deserializer.ReadBytes(64);
                        byte[] sharedKey = DiffieHellman.GenerateSharedKey(publicKey, Keys.PrivateKey, Keys.Prime);
                        byte[] randomBytes = Utilities.GetRandomBytes();
                        byte[] hashedRandomBytes = Utilities.Hash(randomBytes);
                        byte[] xoredRandomBytes = Utilities.XOR(randomBytes, sharedKey);
                        this.RC4Key = hashedRandomBytes;
                        this.SendKey(xoredRandomBytes);
                        this.NetworkSession.DataReceived -= OnDataReceived;
                        this.OnHandshakeDone();
                        break;
                    default:
                        Console.WriteLine("Unhandled packet, header: {0}, length: {1}", header, length);
                        break;
                }
            }
        }

        private void SendKey(byte[] key)
        {
            Serializer serializer = new Serializer();
            serializer.Write((byte)0x01); // RC4Seed Header
            serializer.Write((byte)0x16); // RC4Seed Length
            serializer.Write(key); // RC4Key
            this.NetworkSession.Send(serializer.GetBytes());
        }

        private void OnHandshakeDone()
        {
            if (this.HandshakeDone != null)
            {
                this.HandshakeDone();
            }
        }
    }
}
