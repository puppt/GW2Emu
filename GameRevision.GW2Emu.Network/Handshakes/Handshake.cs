using System;
using GameRevision.GW2Emu.Core.Serializers;

namespace GameRevision.GW2Emu.Network.Handshakes
{
    public abstract class Handshake
    {
        public event Action HandshakeDone;
        public INetworkSession NetworkSession { get; protected set; }
        public uint Version { get; protected set; }
        public byte[] RC4Key { get; protected set; }

        public Handshake(INetworkSession networkSession)
        {
            this.NetworkSession = networkSession;
            this.NetworkSession.DataReceived += OnDataReceived;
        }

        public abstract void OnDataReceived(object sender, DataReceivedEventArgs e);

        protected void SendKey(byte[] key)
        {
            Serializer serializer = new Serializer();
            serializer.Write((byte)0x01); // RC4Seed Header
            serializer.Write((byte)0x16); // RC4Seed Length
            serializer.Write(key); // RC4Key
            this.NetworkSession.Send(serializer.GetBytes());
        }

        protected void OnHandshakeDone()
        {
            if (this.HandshakeDone != null)
            {
                this.HandshakeDone();
            }
        }
    }
}
