using System;
using GameRevision.GW2Emu.Common.Messaging;

namespace GameRevision.GW2Emu.Common.Session
{
    public interface ISession
    {
        void Disconnect();
		void Send(IMessage message);
        void Send(byte[] buffer);
    }
}
