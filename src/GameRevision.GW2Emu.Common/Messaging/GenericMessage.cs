using System;

namespace GameRevision.GW2Emu.Common
{
    public class GenericMessage : IMessage
    {
        // TODO: Fix me once the interface is done.

        public IMessage Deserialize(byte[] data)
        {
            return null;
        }

        public byte[] Serialize()
        {
            return new byte[] {};
        }
    }
}
