using System;
using System.Net;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.LoginServer.Handlers;
using GameRevision.GW2Emu.LoginServer.Session;

namespace GameRevision.GW2Emu.LoginServer
{
    public class LoginServerApp
    {
        private IEventAggregator eventAggregator;
        private ClientListener clientListener;

        public LoginServerApp()
        {
            this.eventAggregator = new EventAggregator();

            this.clientListener = new ClientListener(IPAddress.Any, 6112);
            this.clientListener.ClientConnected += OnClientConnected;
        }

        public void RegisterHandlers()
        {
            this.eventAggregator.Register(new Login());
        }

        public void Start()
        {
            this.clientListener.Listen();

            while (this.clientListener.Listening)
            {
                // do something useful here, eg. read simple server commands
            }
        }

        public void Stop()
        {
            this.clientListener.Stop();
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            GenericSession session = new LoginSession(e.Client, this.eventAggregator);
            session.Start();
        }
    }
}
