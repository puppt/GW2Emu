using System;

namespace GameRevision.GW2Emu.Core
{
    public static class ServerAppExecutor
    {
        public static void RunConsole(IServerApp serverApp)
        {
            Console.Title = serverApp.Name;
            serverApp.RegisterHandlers();
            serverApp.Run();
        }
    }
}
