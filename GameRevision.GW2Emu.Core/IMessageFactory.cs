using System;

namespace GameRevision.GW2Emu.Core
{
    public interface IMessageFactory
    {
        ITriggerableMessage CreateMessage(ushort header);
    }
}
