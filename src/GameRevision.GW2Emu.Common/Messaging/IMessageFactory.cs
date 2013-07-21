using System;
using System.Collections.Generic;

namespace GameRevision.GW2Emu.Common.Messaging
{
    public interface IMessageFactory
    {
        ICollection<IMessage> CreateMessages(byte[] data);
    }
}

