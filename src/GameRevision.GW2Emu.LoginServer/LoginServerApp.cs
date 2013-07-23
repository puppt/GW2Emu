using System;
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
        private ClientManager clientManager;


        public LoginServerApp()
        {
            this.eventAggregator = new EventAggregator();
            this.clientManager = new ClientManager(6112);

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
        public void NewClientHandler(object sender, NewClientEventArgs e)
        {
            GenericSession session = new LoginSession(e.Client, this.eventAggregator);

            e.Client.OnNewData += session.NewDataHandler;
            e.Client.OnLostClient += session.LostClientHandler;
        }


        private void RegisterHandlers()
        {
            this.eventAggregator.Register(new Login());
        }
    }
}
