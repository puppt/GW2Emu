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
        private ConcurrentClientCollection clientCollection;

        public GameServerApp()
        {
            this.eventAggregator = new EventAggregator();

            this.clientListener = new ClientListener(IPAddress.Any, 6112);
            this.clientListener.ClientConnected += OnClientConnected;

            this.clientCollection = new ConcurrentClientCollection();
        }

        public void RegisterHandlers()
        {
            // TODO: implement game server handlers
        }

        public void Start()
        {
            this.clientListener.Listen();
        }

        public void Stop()
        {
            this.clientListener.Stop();
            this.clientCollection.StopAll();
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            this.clientCollection.Add(e.Client);

            GenericSession session = new GameSession(e.Client, this.eventAggregator);
            session.Start();
        }
    }
}

