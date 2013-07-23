using System;

namespace GameRevision.GW2Emu.Common.Network
{
    public class NewDataEventArgs : EventArgs
    {
        public Client Client { get; private set; }
        public byte[] Buffer { get; private set; }
        public int DataLen { get; private set; }

        public NewDataEventArgs(Client client, byte[] buffer, int dataLen)
        {
            this.Client = client;
            this.Buffer = buffer;
            this.DataLen = dataLen;
        }
    }
}
