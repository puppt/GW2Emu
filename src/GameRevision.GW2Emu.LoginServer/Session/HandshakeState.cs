using System;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common.Serialization;
using GameRevision.GW2Emu.Common.Cryptography;
using GameRevision.GW2Emu.Common.Messaging;
using GameRevision.GW2Emu.Common.Session;

namespace GameRevision.GW2Emu.LoginServer.Session
{
    public class HandshakeState : ISessionState
    {

        uint version;
        byte[] key;


        public IEnumerable<IMessage> Deserialize(ISession session, byte[] buffer, int dataLen)
        {
            Deserializer deserializer = new Deserializer(buffer);

            while (deserializer.BaseStream.Position < deserializer.BaseStream.Length)
            {
                byte header = deserializer.ReadByte();
                byte length = deserializer.ReadByte();

                switch (length)
                {
                    case 0x04: // Version
                        deserializer.BaseStream.Position += 2;
                        version = deserializer.ReadUInt32();
                        deserializer.BaseStream.Position += 8;
                        break;

                    case 0x42: // PublicKey
                        byte[] publicKey = deserializer.ReadBytes(64);
                        byte[] sharedKey = DiffieHellman.GenerateSharedKey(publicKey, Keys.PrivateKey, Keys.Prime);
                        byte[] randomBytes = CryptoUtils.GetRandomBytes();
                        byte[] hashedRandomBytes = CryptoUtils.Hash(randomBytes);
                        byte[] xoredRandomBytes = CryptoUtils.XOR(randomBytes, sharedKey); // TODO: Is this unused?!
                        key = hashedRandomBytes;
                        
                        session.Send(
                            BinaryBuilder
                                 .Start()
                                 .Write((byte)0x01)
                                 .Write((byte)0x16)
                                 .Write(key)
                                 .Finish());
                        break;

                    default:
                        Console.WriteLine("Unhandled packet, header: {0}, length: {1}", header, length);
                        break;
                }
            }

            return new IMessage[] {};
        }
    }
}
