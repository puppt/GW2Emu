using System;
using System.Net;

namespace GameRevision.GW2Emu.Core
{
    public interface ISessionListener : IDisposable
    {
        event EventDesign.SessionCreatedEventHandler SessionCreated;
        IPEndPoint EndPoint { get; }
        bool Listening { get; }
        void Listen();
    }
}
