using System;
using System.Net;

namespace GameRevision.GW2Emu.Core
{
    /// <summary>
    /// Communicates with the Guild Wars 2 client through the parent ISession.
    /// </summary>
    public interface INetworkSession : IDisposable
    {
        event EventDesign.DataReceivedEventHandler DataReceived;
        IPEndPoint RemoteEndPoint { get; }
        IPEndPoint LocalEndPoint { get; }
        bool Running { get; }
        void Run();
    }
}
