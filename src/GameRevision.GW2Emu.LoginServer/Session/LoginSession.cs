using System;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.LoginServer.Messages;

namespace GameRevision.GW2Emu.LoginServer.Session
{
    public class LoginSession : GenericSession
    {
        public LoginSession(Client client, IEventAggregator aggregator) : base(client, aggregator, new ClientMessageFactory(), new LoginHandshake(client))
        {
        }
    }
}
