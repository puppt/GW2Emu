using System;
using System.Net;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.LoginServer.Handlers;
using GameRevision.GW2Emu.LoginServer.Session;
using NLog;

namespace GameRevision.GW2Emu.LoginServer
{
    public class LoginServerApp
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IEventAggregator eventAggregator;
        private ClientListener clientListener;

        public LoginServerApp()
        {
            this.eventAggregator = new EventAggregator();

            this.clientListener = new ClientListener(IPAddress.Any, 8112);
            this.clientListener.ClientConnected += OnClientConnected;

            logger.Info("Server created on {0}:{1}.", this.clientListener.EndPoint.Address, this.clientListener.EndPoint.Port);
        }

        public void RegisterHandlers()
        {
            this.eventAggregator.Register(new Login());

            logger.Info("Registered handlers.");
        }

        public void Start()
        {
            this.clientListener.Listen();

            logger.Info("Server started.");
        }

        public void Stop()
        {
            this.clientListener.Stop();

            logger.Info("Server stopped.");
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            GenericSession session = new LoginSession(e.Client, this.eventAggregator);
            session.Start();
        }
    }
}
