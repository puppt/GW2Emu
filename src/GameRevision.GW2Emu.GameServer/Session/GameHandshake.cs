using System;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.GameServer.Session
{
    public class GameHandshake : Handshake
    {
        public uint ClientVersion { get; private set; }

        public GameHandshake(Client client) : base(client)
        {
        }

        protected override void HandlePacket(byte header, byte length, Deserializer deserializer)
        {
            if (length == 0x05)
            {
                deserializer.AppendPosition(2);
                this.ClientVersion = deserializer.ReadUInt32();
                deserializer.AppendPosition(78);
            }
        }
    }
}
