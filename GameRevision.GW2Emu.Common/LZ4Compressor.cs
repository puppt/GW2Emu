using System;
using GameRevision.GW2Emu.Common.Serialization;
using LZ4Sharp;

namespace GameRevision.GW2Emu.Common
{
    public class LZ4Compressor
    {
        private ILZ4Compressor compressor;

        public LZ4Compressor()
        {
            if (IntPtr.Size == 4)
            {
                compressor = new LZ4Compressor32();
            }
            else
            {
                compressor = new LZ4Compressor64();
            }
        }

        public byte[] Compress(byte[] buffer)
        {
            byte[] compressed = compressor.Compress(buffer);

            return BinaryBuilder
                .Start()
                .Write((short)compressed.Length)
                .Write((short)buffer.Length)
                .Write(compressed)
                .Finish();
        }
    }
}
