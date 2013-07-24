using System;

namespace GameRevision.GW2Emu.GameServer
{
    public static class Program
    {
        public static void Main()
        {
            GameServerApp serverApp = new GameServerApp();

            // register the event handlers
            serverApp.RegisterHandlers();

            // start the server
            serverApp.Start();

            // add shutdown hook to close & disconnect the server 
            // gracefully if the application is killed
            AppDomain.CurrentDomain.ProcessExit += delegate
            { 
                serverApp.Stop();
                Console.WriteLine("Server is terminating.");
            };
        }
    }
}
