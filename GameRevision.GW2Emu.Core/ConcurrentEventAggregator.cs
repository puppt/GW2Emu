using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace GameRevision.GW2Emu.Core
{
    /// <summary>
    /// EventAggregator class.
    /// This class represents a multi threaded IEventAggregator implementation
    /// that uses a minimum of reflection and the EventHandler generic delegate type.
    /// This class will fork a triggered event into multiple tasks, each of them being
    /// defined by one of the registered handlers.
    /// For more information see IEventAggregator.
    /// </summary>
    public class ConcurrentEventAggregator : IEventAggregator
    {
        private IDictionary<Type, IList<EventHandler<IEvent>>> handlers;

        public ConcurrentEventAggregator()
        {
            this.handlers = new ConcurrentDictionary<Type, IList<EventHandler<IEvent>>>();
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

                                if (!this.handlers.ContainsKey(parameterType))
                                {
                                    this.handlers[parameterType] = new List<EventHandler<IEvent>>();
                                }

                                var handlerList = this.handlers[parameterType];

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
            if (!this.handlers.ContainsKey(typeof(T)))
            {
                this.handlers[typeof(T)] = new List<EventHandler<IEvent>>();
            }

            var handlerList = this.handlers[typeof(T)];

            handlerList.Add(delegate(IEvent evt)
            {
                handler((T)evt);
            });
        }

        public void Trigger(IEvent evt)
        {
            IList<EventHandler<IEvent>> handlerList;

            if (this.handlers.TryGetValue(evt.GetType(), out handlerList))
            {
                Parallel.ForEach(handlerList, handler =>
                {
                    handler.Invoke(evt);
                });
            }
        }
    }
}