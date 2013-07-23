using System;
using System.Collections.Generic;

namespace GameRevision.GW2Emu.Common.Messaging
{
    public interface IMessageFactory
    {
        IEnumerable<IMessage> CreateMessages(byte[] data);
    }
}

