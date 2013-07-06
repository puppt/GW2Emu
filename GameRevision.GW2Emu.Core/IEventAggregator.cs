using System;

namespace GameRevision.GW2Emu.Core
{
    /// <summary>
    /// Author: Ephe-Meral
    /// IEventAggregator interface.
    /// This interface defines what basic functionalities are provided by the EventAggregator.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// This method registers the specified registerable.
        /// </summary>
        /// <param name="registerable">
        /// The registerable object.
        /// </param>
        void Register(IRegisterable registerable);

        /// <summary>
        /// This method registers the specified handler, and associates
        /// it with the specified event type T.
        /// </summary>
        /// <param name="handler">
        /// The EventHandler method.
        /// </param>
        /// <typeparam name="T">
        /// The concrete type that implements IEvent.
        /// </typeparam>
        void Register<T>(EventHandler<T> handler) where T : IEvent;

        /// <summary>
        /// This method triggers the specified concrete event.
        /// This will call all type-associated handler delegates and pass
        /// evt as a parameter to them.
        /// </summary>
        /// <param name="evt">
        /// The event object that implements IEvent.
        /// </param>
        void Trigger(IEvent evt);
    }
}
