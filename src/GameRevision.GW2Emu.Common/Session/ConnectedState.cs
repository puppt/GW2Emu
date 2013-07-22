using System;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Cryptography;
using GameRevision.GW2Emu.Common.Messaging;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.Common.Session
{
    public class ConnectedState : ISessionState
    {

        private RC4Encryptor encryptor;
        private IMessageFactory messageFactory;
        private LZ4Compressor compressor;


        public ConnectedState(RC4Encryptor encryptor, IMessageFactory messageFactory)
        {
            this.encryptor = encryptor;
            this.messageFactory = messageFactory;

            this.compressor = new LZ4Compressor();
        }


        public IEnumerable<IMessage> Deserialize(ISession session, byte[] data)
        {
            // preprocessing...
            byte[] buffer = this.encryptor.Decrypt(data);

            // let the factory create the messages
            var result = messageFactory.CreateMessages(data);

            foreach (var msg in result)
            {
                // postprocessing...
                msg.Owner = session;
            }

            return result;
        }


        public byte[] Serialize(ISession session, IMessage message)
        {
            // serialize the data into a bytestream
            Serializer serializer = new Serializer();
            message.Serialize(serializer);

            // postprocessing...
            byte[] compressed = this.compressor.Compress(serializer.GetBytes());
            byte[] encrypted = this.encryptor.Encrypt(compressed);

            return encrypted;
        }
    }
}

