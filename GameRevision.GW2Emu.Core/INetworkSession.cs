using System;
using System.Net;
using GameRevision.GW2Emu.Core.EventDesign;

namespace GameRevision.GW2Emu.Core
{
    public interface INetworkSession
    {
        event DataReceivedEventHandler DataReceived;
        IPEndPoint RemoteEndPoint { get; }
        IPEndPoint LocalEndPoint { get; }
        bool Running { get; }
        void Run();
        void Stop();
    }
}
