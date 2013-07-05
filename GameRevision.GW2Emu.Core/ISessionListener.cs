using System;
using System.Net;
using GameRevision.GW2Emu.Core.EventDesign;

namespace GameRevision.GW2Emu.Core
{
    public interface ISessionListener
    {
        event ClientConnectedEventHandler ClientConnected;
        IPEndPoint EndPoint { get; }
        bool Listening { get; }
        void Listen();
        void Stop();
    }
}
