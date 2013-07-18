using System;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.LoginServer.Session;
using GameRevision.GW2Emu.Common.Messaging;

namespace GameRevision.GW2Emu.LoginServer.Session
{
    public class LoginSession : GenericSession
    {
        IEventAggregator aggregator;


        public LoginSession(Client client, IEventAggregator aggregator) : base(client)
        {
            this.aggregator = aggregator;

            State = new HandshakeState();
        }


        void Trigger(IMessage message)
        {
            aggregator.Trigger(message);
        }


//        private void OnHandshakeDone()
//        {
//            this.Compressor = new LZ4Compressor();
//            this.Encryptor = new RC4Encryptor(this.Handshake.RC4Key);
//            this.NetworkSession.DataReceived += OnDataReceived;
//        }
//
//        public void OnDataReceived(object sender, NewDataEventArgs e)
//        {
//            byte[] buffer = this.Encryptor.Decrypt(e.Buffer);
//            Deserializer deserializer = new Deserializer(buffer);
//
//            while (deserializer.BaseStream.Position < deserializer.BaseStream.Length)
//            {
//                ushort header = deserializer.ReadUInt16();
//                ITriggerableMessage message = this.MessageFactory.CreateMessage(header);
//                message.Session = this;
//                message.Deserialize(deserializer);
//                message.TriggerEvent(this.ServerApp.EventAggregator);
//            }
//        }
//
//        public void Send(IMessage message)
//        { 
//            Serializer serializer = new Serializer();
//            message.Serialize(serializer);
//            byte[] compressed = this.Compressor.Compress(serializer.GetBytes());
//            byte[] encrypted = this.Encryptor.Encrypt(compressed);
//            this.NetworkSession.Send(encrypted);
//        }
    }
}
