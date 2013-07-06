using System;
using GameRevision.GW2Emu.Core.Serializers;

namespace GameRevision.GW2Emu.Network.Handshakes
{
    public class GameHandshake
    {
        public event Action HandshakeDone;
        public int Version { get; private set; }

        private bool done;

        public GameHandshake(INetworkSession networkSession)
        {
            networkSession.DataReceived += OnDataReceived;
        }

        public void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!this.done)
            {
                Deserializer deserializer = new Deserializer(e.Buffer);

                byte header = deserializer.ReadByte();
                byte length = deserializer.ReadByte();

                // Implement game handshake here:

                switch (length)
                {
                    case 0x05: // Verify
                        break;
                    case 0x42: // PublicKey
                        break;
                }
            }
            else
            {
                ((INetworkSession)sender).DataReceived -= OnDataReceived;

                if (this.HandshakeDone != null)
                {
                    this.HandshakeDone();
                }
            }
        }
    }
}
