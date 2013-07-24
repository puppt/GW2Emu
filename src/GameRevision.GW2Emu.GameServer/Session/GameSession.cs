using System;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.GameServer.Messages;

namespace GameRevision.GW2Emu.GameServer.Session
{
    public class GameSession : GenericSession
    {
        public GameSession(Client client, IEventAggregator aggregator) : base(client, aggregator, new ClientMessageFactory(), new GameHandshake(client))
        {
        }
    }
}
