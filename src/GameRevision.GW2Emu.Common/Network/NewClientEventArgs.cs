using System;

namespace GameRevision.GW2Emu.Common.Network
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
