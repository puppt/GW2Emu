using System;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.Common
{
    public interface IMessage
    {
        // TODO: Come up with a decent interface!
        IMessage Deserialize(byte[] data);
        byte[] Serialize();
    }
}
