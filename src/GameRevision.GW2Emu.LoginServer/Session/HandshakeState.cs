using System;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common.Serialization;
using GameRevision.GW2Emu.Common.Cryptography;
using GameRevision.GW2Emu.Common.Messaging;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.LoginServer.Messages;

namespace GameRevision.GW2Emu.LoginServer.Session
{
    public class HandshakeState : ISessionState
    {

        //private uint clientVersion;
        private byte[] key;


        public IEnumerable<IMessage> Deserialize(ISession session, byte[] data)
        {
            Deserializer deserializer = new Deserializer(data);

            while (deserializer.BaseStream.Position < deserializer.BaseStream.Length)
            {
                byte header = deserializer.ReadByte();
                byte length = deserializer.ReadByte();

                switch (length)
                {
                    case 0x04: // Version
                        deserializer.BaseStream.Position += 2;
                        /*this.clientVersion = */deserializer.ReadUInt32();
                        deserializer.BaseStream.Position += 8;
                        break;

                    case 0x42: // PublicKey
                        byte[] publicKey = deserializer.ReadBytes(64);
                        byte[] sharedKey = DiffieHellman.GenerateSharedKey(publicKey, Keys.PrivateKey, Keys.Prime);
                        byte[] randomBytes = CryptoUtils.GetRandomBytes();
                        byte[] hashedRandomBytes = CryptoUtils.Hash(randomBytes);
                        byte[] xoredRandomBytes = CryptoUtils.XOR(randomBytes, sharedKey);
                        key = hashedRandomBytes;
                        
                        Serializer serializer = new Serializer();
                        serializer.Write((byte)0x01); // RC4Seed Header
                        serializer.Write((byte)0x16); // RC4Seed Length
                        serializer.Write(xoredRandomBytes); // xored RC4Key

                        session.Send(serializer.GetBytes());

                        session.State = prepareNewState();

                        break;

                    default:
                        Console.WriteLine("Unhandled packet, header: {0}, length: {1}", header, length);
                        break;
                }
            }

            // mock up, since we return the byte-encoded messages directly
            return new IMessage[] {};
        }


        public byte[] Serialize(ISession session, IMessage message) { return new byte[] {}; }


        private ConnectedState prepareNewState()
        {
            return new ConnectedState(new RC4Encryptor(this.key), new ClientMessageFactory());
        }
    }
}
