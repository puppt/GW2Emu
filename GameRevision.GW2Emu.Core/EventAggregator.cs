using System;
using System.Reflection;
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

        public void Register(Type[] types)
        {
            foreach (Type type in types)
            {
                if (type.GetConstructor(Type.EmptyTypes) != null)
                {
                    object target = null;
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        ParameterInfo[] parameters = method.GetParameters();
                        if (parameters.Length == 1 && method.Attributes > 0)
                        {
                            Type parameterType = parameters[0].ParameterType;
                            object[] attributes = method.GetCustomAttributes(typeof(EventHandlerAttribute), false);
                            if (attributes.Length == 1)
                            {
                                if (target == null)
                                {
                                    target = Activator.CreateInstance(type);
                                }

                                if (!handlers.ContainsKey(parameterType))
                                {
                                    handlers[parameterType] = new List<EventHandler<IEvent>>();
                                }

                                var handlerList = handlers[parameterType];

                                // HACK: Find a fix for this problem.
                                MethodInfo methodInfo = method;

                                handlerList.Add(delegate(IEvent evt)
                                {
                                    // Not possible to just call method.Invoke for some strange reason.
                                    // The local variable 'method' turns into an instance of 'System.Type.GetType()'
                                    methodInfo.Invoke(target, new object[] { evt });
                                });
                            }
                        }
                    }
                }
            }
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