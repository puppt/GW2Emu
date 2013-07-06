using System;
using System.Net.Sockets;

namespace GameRevision.GW2Emu.Network
{
    public class NetworkSessionCreatedEventArgs : EventArgs
    {
        public INetworkSession NetworkSession { get; private set; }
        public DateTime ConnectionTime { get; private set; }

        public NetworkSessionCreatedEventArgs(INetworkSession networkSession, DateTime connectionTime)
        {
            this.NetworkSession = networkSession;
            this.ConnectionTime = connectionTime;
        }
    }
}
