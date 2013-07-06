using System;

namespace GameRevision.GW2Emu.Core
{
    public interface ITriggerableMessage : IMessage, IEvent
    {
        void TriggerEvent(IEventAggregator aggregator);
    }
}
