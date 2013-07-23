using System;
using GameRevision.GW2Emu.Common.Messaging;

namespace GameRevision.GW2Emu.Common.Session
{
    public interface ISession
    {
        ISessionState State { set; }

        void Kick();

		void Send(IMessage message);

        void Send(byte[] data);
    }
}
