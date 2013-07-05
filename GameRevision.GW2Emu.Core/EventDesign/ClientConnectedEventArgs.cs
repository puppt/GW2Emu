using System;
using System.Net.Sockets;

namespace GameRevision.GW2Emu.Core.EventDesign
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public INetworkSession NetworkSession { get; private set; }
        public DateTime ConnectionTime { get; private set; }

        public ClientConnectedEventArgs(INetworkSession networkSession, DateTime connectionTime)
        {
            this.NetworkSession = networkSession;
            this.ConnectionTime = connectionTime;
        }
    }
}
