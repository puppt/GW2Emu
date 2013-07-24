using System;

namespace GameRevision.GW2Emu.Common.Network
{
    public class DataReceivedEventArgs : EventArgs
    {
        public byte[] Buffer { get; private set; }

        public DataReceivedEventArgs(byte[] buffer)
        {
            this.Buffer = buffer;
        }
    }
}
