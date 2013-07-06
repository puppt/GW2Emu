using System;

namespace GameRevision.GW2Emu.Core
{
    public interface ISession
    {
        IServerApp ServerApp { get; }
        void SendMessage(IMessage message);
        void Run();
        void Stop();
    }
}
