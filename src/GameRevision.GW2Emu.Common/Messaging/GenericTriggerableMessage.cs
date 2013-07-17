using System;
using GameRevision.GW2Emu.Common.Events;

namespace GameRevision.GW2Emu.Common
{
    public class GenericTriggerableMessage : GenericMessage, ITriggerableMessage
    {
        public void TriggerEvent(IEventAggregator aggregator)
        {
            aggregator.Trigger(this);
        }
    }
}
