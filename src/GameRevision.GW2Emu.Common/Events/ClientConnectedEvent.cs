using System;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Events;

namespace GameRevision.GW2Emu.Common.Events
{
    public class ClientConnectedEvent : IEvent
    {
        public ISession Session { get; private set; }


        public ClientConnectedEvent(ISession session)
        {
            this.Session = session;
        }
    }
}

