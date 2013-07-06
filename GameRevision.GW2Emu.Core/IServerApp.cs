using System;

namespace GameRevision.GW2Emu.Core
{
    public interface IServerApp
    {
        IEventAggregator EventAggregator { get; }
        ConcurrentSessionCollection SessionCollection { get; }
        string Name { get; }
        void RegisterHandlers();
        void Run();
        void Stop();
    }
}
