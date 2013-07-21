using System;
using GameRevision.GW2Emu.Common.Serialization;
using GameRevision.GW2Emu.Common.Cryptography;

//namespace GameRevision.GW2Emu.Network.Handshakes
//{
//    public class GameHandshake : Handshake
//    {
//        public GameHandshake(INetworkSession networkSession) : base(networkSession)
//        {
//        }
//
//        public override void OnDataReceived(object sender, DataReceivedEventArgs e)
//        {
//            Deserializer deserializer = new Deserializer(e.Buffer);
//
//            while (deserializer.BaseStream.Position < deserializer.BaseStream.Length)
//            {
//                byte header = deserializer.ReadByte();
//                byte length = deserializer.ReadByte();
//
//                switch (length)
//                {
//                    case 0x05: // Verify
//                        deserializer.BaseStream.Position += 2;
//                        this.Version = deserializer.ReadUInt32();
//                        deserializer.BaseStream.Position += 56; // Probably wrong offset
//                        break;
//                    case 0x42: // PublicKey
//                        byte[] publicKey = deserializer.ReadBytes(64);
//                        byte[] sharedKey = DiffieHellman.GenerateSharedKey(publicKey, Keys.PrivateKey, Keys.Prime);
//                        byte[] randomBytes = Utilities.GetRandomBytes();
//                        byte[] hashedRandomBytes = Utilities.Hash(randomBytes);
//                        byte[] xoredRandomBytes = Utilities.XOR(randomBytes, sharedKey);
//                        this.RC4Key = hashedRandomBytes;
//                        this.SendKey(xoredRandomBytes);
//                        this.NetworkSession.DataReceived -= OnDataReceived;
//                        this.OnHandshakeDone();
//                        break;
//                }
//            }
//        }
//    }
//}
