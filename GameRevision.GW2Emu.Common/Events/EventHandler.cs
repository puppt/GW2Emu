using System;

namespace GameRevision.GW2Emu.Common.Events
{
    public delegate void EventHandler<T>(T evt) where T : IEvent;
}
