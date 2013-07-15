using System;

namespace GameRevision.GW2Emu.Common
{
	public interface ISessionState
	{
		void HandleData(byte[] buffer, int dataLen);
	}
}

