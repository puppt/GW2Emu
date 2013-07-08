using System;
using GameRevision.GW2Emu.Core;
using GameRevision.GW2Emu.Core.Serializers;
using GameRevision.GW2Emu.Core.Compression;
using GameRevision.GW2Emu.Core.Cryptography;
using GameRevision.GW2Emu.Network;
using GameRevision.GW2Emu.Network.Handshakes;
using GameRevision.GW2Emu.Messages.LoginServer;

namespace GameRevision.GW2Emu.LoginServer
{
    public class LoginSession : ISession
    {
        public IServerApp ServerApp { get; private set; }
        public INetworkSession NetworkSession { get; private set; }
        public IMessageFactory MessageFactory { get; private set; }
        public LoginHandshake Handshake { get; private set; }
        public LZ4Compressor Compressor { get; private set; }
        public RC4Encryptor Encryptor { get; private set; }

        public LoginSession(IServerApp serverApp, INetworkSession networkSession)
        {
            this.ServerApp = serverApp;
            this.NetworkSession = networkSession;
            this.MessageFactory = new LoginMessageFactory();
            this.Handshake = new LoginHandshake(this.NetworkSession);
            this.Handshake.HandshakeDone += OnHandshakeDone;
        }

        public void Run()
        {
            this.NetworkSession.Run();
        }

        private void OnHandshakeDone()
        {
            this.Compressor = new LZ4Compressor();
            this.Encryptor = new RC4Encryptor(this.Handshake.RC4Key);
            this.NetworkSession.DataReceived += OnDataReceived;
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            byte[] buffer = this.Encryptor.Decrypt(e.Buffer);
            Deserializer deserializer = new Deserializer(buffer);
            ushort header = deserializer.ReadUInt16();
            ITriggerableMessage message = this.MessageFactory.CreateMessage(header);
            message.Session = this;
            message.Deserialize(deserializer);
            message.TriggerEvent(this.ServerApp.EventAggregator);
        }

        public void SendMessage(IMessage message)
        {
            Serializer serializer = new Serializer();
            message.Serialize(serializer);
            byte[] compressed = this.Compressor.Compress(serializer.GetBytes());
            byte[] encrypted = this.Encryptor.Encrypt(compressed);
            this.NetworkSession.Send(encrypted);
        }

        public void Stop()
        {
            this.NetworkSession.Stop();
            this.ServerApp.SessionCollection.Remove(this);
        }
    }
}
