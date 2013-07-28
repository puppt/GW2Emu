using System;
using System.Net;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.GameServer.Session;
using NLog;

namespace GameRevision.GW2Emu.GameServer
{
    public class GameServerApp
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IEventAggregator eventAggregator;
        private ClientListener clientListener;

        public GameServerApp()
        {
            this.eventAggregator = new EventAggregator();

            this.clientListener = new ClientListener(IPAddress.Any, 9112);
            this.clientListener.ClientConnected += OnClientConnected;

            logger.Info("Server created on {0}:{1}.", this.clientListener.EndPoint.Address, this.clientListener.EndPoint.Port);
        }

        public void RegisterHandlers()
        {
            // TODO: implement game server handlers

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
            GenericSession session = new GameSession(e.Client, this.eventAggregator);
            session.Start();
        }
    }
}

