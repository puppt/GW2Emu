using System;
using System.Net.Sockets;

namespace GameRevision.GW2Emu.Network
{
    public class NewClientEventArgs : EventArgs
    {
        public Client Client { get; private set; }

        public NewClientEventArgs(Client client)
        {
            this.Client = client;
        }
    }
}
