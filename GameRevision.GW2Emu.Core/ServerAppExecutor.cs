using System;

namespace GameRevision.GW2Emu.Core
{
    public static class ServerAppExecutor
    {
        /*
        public enum Version
        {
            Alpha,
            Beta,
            RC1,
            RC2,
            Release
        }
        */

        public static void RunConsole(IServerApp serverApp)
        {
            Console.Title = serverApp.Name;
            serverApp.RegisterHandlers();
            serverApp.Run();
        }

        /*
        public static void RunConsole(IServerApp serverApp, Version version)
        {
            Console.Title = string.Format("{0} [{1}]", serverApp.Name, version);
            serverApp.RegisterHandlers();
            serverApp.Run();
        }
        */
    }
}
