using System;

namespace GameRevision.GW2Emu.GameServer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var serverApp = new GameServerApp();

            // start the server
            serverApp.Start();

            // add shutdown hook to close & disconnect the server 
            // gracefully if the application is killed
            AppDomain.CurrentDomain.ProcessExit += (s, e) => 
            { 
                serverApp.Stop();
                Console.WriteLine("Server is terminating.");
            };
        }
    }
}
