using System;
using GameRevision.GW2Emu.Common.Events;

namespace GameRevision.GW2Emu.Common.Messaging
{
    public interface IMessage : IEvent
    {
        // TODO: Come up with a decent interface!
        IMessage Deserialize(byte[] data);
        byte[] Serialize();
    }
}
