using System;
using System.Net;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.GameServer.Session;

namespace GameRevision.GW2Emu.GameServer
{
    public class GameServerApp
    {
        private IEventAggregator eventAggregator;
        private ClientListener clientListener;

        public GameServerApp()
        {
            this.eventAggregator = new EventAggregator();

            this.clientListener = new ClientListener(IPAddress.Any, 7112);
            this.clientListener.ClientConnected += OnClientConnected;
        }

        public void RegisterHandlers()
        {
            // TODO: implement game server handlers
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
            GenericSession session = new GameSession(e.Client, this.eventAggregator);
            session.Start();
        }
    }
}

