using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Math;

namespace GameRevision.GW2Emu.Common.Serialization
{
    public class BinaryBuilder
    {

        BinaryWriter writer;


        private BinaryBuilder(BinaryWriter writer)
        {
            this.writer = writer;
        }


        public static BinaryBuilder Start()
        {
            return new BinaryBuilder(new BinaryWriter(new MemoryStream()));
        }


        public BinaryBuilder Write(Boolean value)   { writer.Write(value); return this; }
        public BinaryBuilder Write(byte value)      { writer.Write(value); return this; }
        public BinaryBuilder Write(byte[] value)    { writer.Write(value); return this; }
        public BinaryBuilder Write(int value)       { writer.Write(value); return this; }
        public BinaryBuilder Write(uint value)      { writer.Write(value); return this; }
        public BinaryBuilder Write(float value)     { writer.Write(value); return this; }


        public BinaryBuilder Write(Vector2 value)
        {
            Write(value.X);
            Write(value.Y);
        
            return this;
        }

        public BinaryBuilder Write(Vector3 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        
            return this;
        }


        public BinaryBuilder Write(Vector4 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);

            return this;
        }

        public BinaryBuilder Write(WorldPosition value)
        {
            Write(value.Vector);
            WriteVarint(value.W);
        
            return this;
        }


        public BinaryBuilder Write(UID value)
        {
            Write(value.Data);

            return this;
        }


        public BinaryBuilder Write(EndPoint value)
        {
            SocketAddress socketAddress = value.Serialize();
            byte[] socketAddressBytes = new byte[28];

            for (int i = 0; i < socketAddress.Size; i++)
            {
                socketAddressBytes[i] = socketAddress[i];
            }

            Write(socketAddressBytes);

            return this;
        }


        public BinaryBuilder WriteVarint(int value)
        {
            bool first = true;

            while (first || value > 0)
            {
                first = false;
                byte lower7bits = (byte)(value & 0x7f);
                value >>= 7;

                if (value > 0)
                {
                    lower7bits |= 128;
                }

                Write(lower7bits);
            }

            return this;
        }


        public BinaryBuilder WriteVarbyte(long value, int count)
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(BitConverter.GetBytes(value));
            Write(buffer.GetRange(0, count).ToArray());

            return this;
        }


        public BinaryBuilder WriteEncodedChar(char value, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(new char[] { value });
            Write(data);

            return this;
        }


        public BinaryBuilder WriteUtf16String(string value)
        {
            foreach (char character in value)
            {
                WriteEncodedChar(character, Encoding.Unicode);
            }

            WriteEncodedChar('\0', Encoding.Unicode);

            return this;
        }


        public BinaryBuilder WriteAsciiString(string value)
        {
            foreach (char character in value)
            {
                WriteEncodedChar(character, Encoding.ASCII);
            }

            WriteEncodedChar('\0', Encoding.ASCII);

            return this;
        }


        public byte[] Finish()
        {
            return ((MemoryStream)writer.BaseStream).ToArray();
        }
    }
}
