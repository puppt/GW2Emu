using System;
using System.Net;

namespace GameRevision.GW2Emu.LoginServer
{
    public static class Program
    {
        public static void Main()
        {
            Console.Title = "GW2Emu login server";

            LoginServerApp serverApp = new LoginServerApp();

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
