using System;
using GameRevision.GW2Emu.Common.Events;

namespace GameRevision.GW2Emu.Common
{
    public interface ITriggerableMessage : IMessage, IEvent
    {
        void TriggerEvent(IEventAggregator aggregator);
    }
}
