using System;

namespace GameRevision.GW2Emu.Network
{
    public class LostClientEventArgs : EventArgs
    {
        public Client Client { get; private set; }

        public LostClientEventArgs(Client client)
        {
            this.Client = client;
        }
    }
}

