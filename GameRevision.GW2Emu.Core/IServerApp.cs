using System;

namespace GameRevision.GW2Emu.Core
{
    public interface IServerApp
    {
        ISessionListener SessionListener { get; }
        void Run();
    }
}
