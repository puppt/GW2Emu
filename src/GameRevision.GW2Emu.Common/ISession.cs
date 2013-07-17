using System;

namespace GameRevision.GW2Emu.Common
{
    public interface ISession
    {
        void SetState(ISessionState state);

		void Send(IMessage message);
    }
}
