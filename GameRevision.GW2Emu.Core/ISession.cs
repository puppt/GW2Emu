using System;

namespace GameRevision.GW2Emu.Core
{
    /// <summary>
    /// Represents a Guild Wars 2 client.
    /// </summary>
    public interface ISession
    {
        INetworkSession NetworkSession { get; }
    }
}
