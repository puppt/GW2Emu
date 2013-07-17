using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace GameRevision.GW2Emu.Common.Events
{
    public class EventAggregator : IEventAggregator
    {

        IDictionary<Type, IList<EventHandler<IEvent>>> handlers;


        public EventAggregator()
        {
            handlers = new ConcurrentDictionary<Type, IList<EventHandler<IEvent>>>();
        }


        public void Register(IRegisterable registerable)
        {
            registerable.RegisterMeWith(this);
        }


        public void RegisterAll(IEnumerable<IRegisterable> registerables)
        {
            foreach (var registerable in registerables) 
            {
                Register(registerable);
            }
        }


        public void Register<T>(EventHandler<T> handler) where T : IEvent
        {
            if (!handlers.ContainsKey(typeof(T)))
            {
                handlers[typeof(T)] = new List<EventHandler<IEvent>>();
            }

            var handlerList = handlers[typeof(T)];

            handlerList.Add(evt => handler((T)evt));
        }


        public void Trigger(IEvent evt)
        {
            IList<EventHandler<IEvent>> handlerList;

            if (handlers.TryGetValue(evt.GetType(), out handlerList))
            {
                Parallel.ForEach (handlerList, handler =>
                {
                    handler.Invoke(evt);
                });
            }
        }
    }
}