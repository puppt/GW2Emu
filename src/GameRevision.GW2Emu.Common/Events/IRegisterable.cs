using System;

namespace GameRevision.GW2Emu.Common.Events
{
    public interface IRegisterable
    {
        void RegisterMeWith(IEventAggregator aggregator);
    }
}
