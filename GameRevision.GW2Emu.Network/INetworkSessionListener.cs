using System;
using System.Net;

namespace GameRevision.GW2Emu.Network
{
    public interface INetworkSessionListener
    {
        event EventHandler<NetworkSessionCreatedEventArgs> NetworkSessionCreated;
        IPEndPoint EndPoint { get; }
        bool Listening { get; }
        void Listen();
        void Stop();
    }
}
