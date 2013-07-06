using System;
using GameRevision.GW2Emu.Core.Serializers;
using LZ4Sharp;

namespace GameRevision.GW2Emu.Core.Compression
{
    public class LZ4Compressor
    {
        private ILZ4Compressor compressor;

        public LZ4Compressor()
        {
            if (IntPtr.Size == 4)
            {
                this.compressor = new LZ4Compressor32();
            }
            else
            {
                this.compressor = new LZ4Compressor64();
            }
        }

        public byte[] Compress(byte[] buffer)
        {
            Serializer serializer = new Serializer();
            byte[] compressed = this.compressor.Compress(buffer);
            serializer.Write((short)compressed.Length);
            serializer.Write((short)buffer.Length);
            serializer.Write(compressed);
            return serializer.GetBytes();
        }
    }
}
