using System;

namespace GameRevision.GW2Emu.Network
{
    public class DataReceivedEventArgs : EventArgs
    {
        public byte[] Buffer { get; private set; }
        public int BytesRead { get; private set; }

        public DataReceivedEventArgs(byte[] buffer, int bytesRead)
        {
            this.Buffer = buffer;
            this.BytesRead = bytesRead;
        }
    }
}
