using System;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Events;

namespace GameRevision.GW2Emu.Common.Events
{
    public class ClientDisconnectedEvent : IEvent
    {
        public ISession Session { get; private set; }


        public ClientDisconnectedEvent(ISession session)
        {
            this.Session = session;
        }
    }
}

