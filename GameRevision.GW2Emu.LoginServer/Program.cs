using System;
using GameRevision.GW2Emu.Core;

namespace GameRevision.GW2Emu.LoginServer
{
    public static class Program
    {
        public static void Main()
        {
            IServerApp serverApp = new LoginServerApp();
            ServerAppExecutor.RunConsole(serverApp);
        }
    }
}
