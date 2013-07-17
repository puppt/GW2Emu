using System;
using System.Collections.Generic;

namespace GameRevision.GW2Emu.Common.Events
{
    public interface IEventAggregator
    {
        void Register(IRegisterable registerable);

        void RegisterAll(IEnumerable<IRegisterable> registerables);

        void Register<T>(EventHandler<T> handler) where T : IEvent;

        void Trigger(IEvent evt);
    }
}
