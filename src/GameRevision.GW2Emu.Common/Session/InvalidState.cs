using System;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common.Messaging;

namespace GameRevision.GW2Emu.Common.Session
{
    public class InvalidState: ISessionState
    {

        public IEnumerable<IMessage> Deserialize(ISession session, byte[] data)
        { 
            throw new InvalidOperationException("Cannot deserialize, this session has been kicked.");
        }


        public byte[] Serialize(ISession session, IMessage message)
        { 
            throw new InvalidOperationException("Cannot serialize, this session has been kicked.");
        }
    }
}

