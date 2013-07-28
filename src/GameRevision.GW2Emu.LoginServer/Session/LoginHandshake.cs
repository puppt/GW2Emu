using System;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.LoginServer.Session
{
    public class LoginHandshake : Handshake
    {
        public uint ClientVersion { get; private set; }

        public LoginHandshake(Client client) : base(client)
        {
        }

        protected override bool HandlePacket(byte header, byte length, Deserializer deserializer)
        {
            if (length == 0x04)
            {
                deserializer.AppendPosition(2);
                this.ClientVersion = deserializer.ReadUInt32();
                deserializer.AppendPosition(8);

                return true;
            }

            return false;
        }
    }
}
