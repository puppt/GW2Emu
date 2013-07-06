using System;
using System.Collections.Generic;

namespace GameRevision.GW2Emu.Core
{
    /// <summary>
    /// EventAggregator class.
    /// This class represents a single threaded IEventAggregator implementation
    /// that uses a minimum of reflection and the EventHandler generic delegate type.
    /// For more information see IEventAggregator.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        IDictionary<Type, IList<EventHandler<IEvent>>> handlers;

        public EventAggregator()
        {
            handlers = new Dictionary<Type, IList<EventHandler<IEvent>>>();
        }

        public void Register(IRegisterable registerable)
        {
            registerable.Register(this);
        }

        public void Register<T>(EventHandler<T> handler) where T : IEvent
        {
            if (!handlers.ContainsKey(typeof(T)))
            {
                handlers[typeof(T)] = new List<EventHandler<IEvent>>();
            }

            var handlerList = handlers[typeof(T)];

            handlerList.Add(delegate(IEvent evt)
            {
                handler((T)evt);
            });
        }

        public void Trigger(IEvent evt)
        {
            IList<EventHandler<IEvent>> handlerList;

            if (handlers.TryGetValue(evt.GetType(), out handlerList))
            {
                foreach (EventHandler<IEvent> handler in handlerList)
                {
                    handler.Invoke(evt);
                }
            }
        }
    }
}