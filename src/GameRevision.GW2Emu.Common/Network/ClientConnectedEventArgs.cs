using System;

namespace GameRevision.GW2Emu.Common.Network
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public Client Client { get; private set; }

        public ClientConnectedEventArgs(Client client)
        {
            this.Client = client;
        }
    }
}
