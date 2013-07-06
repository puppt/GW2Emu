using System;
using GameRevision.GW2Emu.Core;

namespace GameRevision.GW2Emu.LoginServer
{
    public class LoginMessageFactory : IMessageFactory
    {
        public ITriggerableMessage CreateMessage(ushort header)
        {
            switch (header)
            {
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
