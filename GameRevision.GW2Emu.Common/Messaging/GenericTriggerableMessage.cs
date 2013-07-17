using System;

namespace GameRevision.GW2Emu.Core
{
    public class GenericTriggerableMessage : GenericMessage, ITriggerableMessage
    {
        public void TriggerEvent(IEventAggregator aggregator)
        {
            aggregator.Trigger(this);
        }
    }
}
