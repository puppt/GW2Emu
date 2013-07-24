using System;

namespace GameRevision.GW2Emu.Common.Events
{
    public interface IRegisterable
    {
        void Register(IEventAggregator aggregator);
    }
}
