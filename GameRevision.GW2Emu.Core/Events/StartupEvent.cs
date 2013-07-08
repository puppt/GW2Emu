using System;

namespace GameRevision.GW2Emu.Core.Events
{
    public class StartupEvent : IEvent
    {
        public IServerApp ServerApp { get; private set; }

        public StartupEvent(IServerApp serverApp)
        {
            this.ServerApp = serverApp;
        }
    }
}
