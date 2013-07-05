using System;

namespace GameRevision.GW2Emu.Core
{
    public interface ISession
    {
        INetworkSession NetworkSession { get; }
    }
}
