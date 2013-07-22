using System;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Messaging;

namespace GameRevision.GW2Emu.LoginServer.Session
{
    public class LoginSession : GenericSession
    {

        // TODO: add whatever properties are needed in the login server per session here
        // but be careful not to introduce too much state!


        public LoginSession(Client client, IEventAggregator aggregator) : base(client, aggregator)
        {
            State = new HandshakeState();
        }
    }
}
