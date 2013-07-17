using System;

namespace GameRevision.GW2Emu.Common
{
	public interface ISessionState
	{
		void HandleData(ISession session, byte[] buffer, int dataLen);
	}
}

