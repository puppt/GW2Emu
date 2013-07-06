using System;

namespace GameRevision.GW2Emu.Core
{
    /// <summary>
    /// EventHandler delegate.
    /// This delegate defines the signature of any event handler method.
    /// If you keep to this, you can register your own methods with the
    /// EventAggregator.
    /// </summary>
    public delegate void EventHandler<T>(T evt) where T : IEvent;
}
