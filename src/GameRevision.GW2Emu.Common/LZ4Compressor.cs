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

            Serializer serializer = new Serializer();
            serializer.Write((short)compressed.Length);
            serializer.Write((short)buffer.Length);
            serializer.Write(compressed);

            return serializer.GetBytes();
        }
    }
}
