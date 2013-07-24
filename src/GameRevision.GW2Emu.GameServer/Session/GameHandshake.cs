using System;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.GameServer.Session
{
    public class GameHandshake : Handshake
    {
        public GameHandshake(Client client) : base(client)
        {
        }

        protected override void HandlePacket(byte header, byte length, Deserializer deserializer)
        {
            throw new NotImplementedException();
        }
    }
}
