using System;

namespace GameRevision.GW2Emu.Network
{
    public class NewDataEventArgs : EventArgs
    {
		public Client Client { get; private set; }
        public byte[] Buffer { get; private set; }
        public int BytesRead { get; private set; }

        public NewDataEventArgs(Client client, byte[] buffer, int bytesRead)
        {
			this.Client = client;
            this.Buffer = buffer;
            this.BytesRead = bytesRead;
        }
    }
}
