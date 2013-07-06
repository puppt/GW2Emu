using System;

namespace GameRevision.GW2Emu.Core
{
    /// <summary>
    /// Author: Ephe-Meral
    /// IRegisterable interface.
    /// This interface is for convenient usage of the EventAggregator
    /// only. You can register a whole object with the aggregator,
    /// by letting it implement this interface and then passing it to
    /// the aggregator.
    /// </summary>
    public interface IRegisterable
    {

        /// <summary>
        /// This method registers this object with the EventAggregator.
        /// You are supposed to take the aggregator parameter and call
        /// the more verbose EventHandler-register method of it, giving
        /// it your own methods.
        /// </summary>
        /// <param name="aggregator">
        /// The local event agggregator instance.
        /// </param>
        void Register(IEventAggregator aggregator);
    }
}
