using System;

namespace GameRevision.GW2Emu.Core.Generic
{
    public class GenericSession : ISession
    {
        public INetworkSession NetworkSession { get; private set; }

        public GenericSession(INetworkSession networkSession)
        {
            this.NetworkSession = networkSession;
        }
    }
}
