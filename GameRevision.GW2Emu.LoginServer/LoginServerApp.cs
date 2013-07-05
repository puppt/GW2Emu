using System;
using System.Net;
using GameRevision.GW2Emu.Core;
using GameRevision.GW2Emu.Core.EventDesign;
using GameRevision.GW2Emu.Network;

namespace GameRevision.GW2Emu.LoginServer
{
    public class LoginServerApp : IServerApp
    {
        public ISessionListener SessionListener { get; private set;  }

        public void Run()
        {
            Console.Title = "Login Server (Test)";

            this.SessionListener = new SessionListener(IPAddress.Any, 80);
            this.SessionListener.ClientConnected += OnClientConnected;
            this.SessionListener.Listen();

            while (this.SessionListener.Listening)
            {
                Console.Write("{0}> ", DateTime.Now.ToString("hh:mm:ss"));
                string command = Console.ReadLine();

                if (command == "stop")
                {
                    this.SessionListener.Stop();
                }
                else if (command == "help")
                {
                    Console.WriteLine();
                    Console.WriteLine(" - Type 'stop' to stop the server");
                    Console.WriteLine();
                }
            }
        }

        private void OnClientConnected(ISessionListener sessionListener, ClientConnectedEventArgs e)
        {
            new LoginSession(e.NetworkSession);
        }
    }
}