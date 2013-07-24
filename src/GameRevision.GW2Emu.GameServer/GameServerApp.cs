using System;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.GameServer.Session;

namespace GameRevision.GW2Emu.GameServer
{
    public class GameServerApp
    {

        private IEventAggregator eventAggregator;
        private ClientListener clientManager;


        public GameServerApp()
        {
            this.eventAggregator = new EventAggregator();
            this.clientManager = new ClientListener(6112);

            this.clientManager.OnNewClient += NewClientHandler;
        }


        public void Start()
        {
            this.clientManager.Start();
        }


        public void Stop()
        {
            this.clientManager.Stop();
        }


        /// <summary>
        /// New client event handler. This is triggered by the network layer.
        /// </summary>
        public void NewClientHandler(object sender, ClientConnectedEventArgs e)
        {
            GenericSession session = new GameSession(e.Client, this.eventAggregator);

            e.Client.OnNewData += session.NewDataHandler;
            e.Client.OnLostClient += session.LostClientHandler;
        }


        private void RegisterHandlers()
        {
            // add the handlers here
        }
    }
}

