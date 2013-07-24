using System;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Messaging;
using GameRevision.GW2Emu.Common.Cryptography;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.Common.Session
{
    public abstract class GenericSession : ISession
    {
        private Client client;
        private IEventAggregator aggregator;
        private IMessageFactory messageFactory;
        private Handshake handshake;
        private RC4Encryptor encryptor;
        private LZ4Compressor compressor;

        public GenericSession(Client client, IEventAggregator aggregator, IMessageFactory messageFactory, Handshake handshake)
        {
            this.client = client;
            this.client.Disconnected += this.OnDisconnected;

            this.aggregator = aggregator;
            this.messageFactory = messageFactory;
            this.handshake = handshake;

            this.handshake.HandshakeDone += delegate
            {
                this.encryptor = new RC4Encryptor(handshake.EncryptionKey);
                this.compressor = new LZ4Compressor();
                this.client.DataReceived += this.OnDataReceived;
            };
        }

        public void Send(IMessage message)
        {
            Serializer serializer = new Serializer();

            message.Serialize(serializer);

            this.Send(serializer.GetBytes());
        }

        public void Send(byte[] buffer)
        {
            byte[] encrypted = this.encryptor.Encrypt(buffer);
            byte[] compressed = this.compressor.Compress(encrypted);
            this.client.Send(compressed);
        }

        public void Start()
        {
            this.client.Run();
        }

        public void Disconnect()
        {
            this.client.Disconnect();
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            byte[] data = this.encryptor.Decrypt(e.Buffer);

            IEnumerable<IMessage> messages = this.messageFactory.CreateMessages(data);

            foreach (IMessage message in messages) 
            {
                message.Session = this;
                this.aggregator.Trigger(message);
            }
        }

        private void OnDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.aggregator.Trigger(new ClientDisconnectedEvent(this));
        }
    }
}

