using System;
using System.Net;

namespace GameRevision.GW2Emu.Network
{
    public interface INetworkSession
    {
        event EventHandler<DataReceivedEventArgs> DataReceived;
        IPEndPoint RemoteEndPoint { get; }
        IPEndPoint LocalEndPoint { get; }
        bool Running { get; }
        void Run();
        void Send(byte[] buffer);
        void Stop();
    }
}
