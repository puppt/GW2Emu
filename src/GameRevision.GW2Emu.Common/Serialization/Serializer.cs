using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using GameRevision.GW2Emu.Common.Math;
using GameRevision.GW2Emu.Common;
using System.Net;

namespace GameRevision.GW2Emu.Common.Serialization
{
    public class Serializer : BinaryWriter
    {

        public Serializer() : base(new MemoryStream())
        {
        }


        public byte[] GetBytes()
        {
            return ((MemoryStream)this.BaseStream).ToArray();
        }


        public void Write(Vector2 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
        }


        public void Write(Vector3 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
            this.Write(value.Z);
        }


        public void Write(Vector4 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
            this.Write(value.Z);
            this.Write(value.W);
        }


        public void Write(WorldPosition value)
        {
            this.Write(value.Vector);
            this.WriteVarint(value.W);
        }


        public void Write(UID value)
        {
            this.Write(value.Data);
        }


        public void Write(IPEndPoint value)
        {
            SocketAddress socketAddress = value.Serialize();
            byte[] socketAddressBytes = new byte[28];

            for (int i = 0; i < socketAddress.Size; i++)
            {
                socketAddressBytes[i] = socketAddress[i];
            }

            this.Write(socketAddressBytes);
        }


        public void WriteVarint(int value)
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

                this.Write(lower7bits);
            }
        }


        public void WriteVarbyte(long value, int count)
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(BitConverter.GetBytes(value));
            this.Write(buffer.GetRange(0, count).ToArray());
        }


        public void WriteEncodedChar(char value, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(new char[] { value });
            this.Write(data);
        }


        public void WriteUtf16String(string value)
        {
            foreach (char character in value)
            {
                this.WriteEncodedChar(character, Encoding.Unicode);
            }

            this.WriteEncodedChar('\0', Encoding.Unicode);
        }


        public void WriteAsciiString(string value)
        {
            foreach (char character in value)
            {
                this.WriteEncodedChar(character, Encoding.ASCII);
            }

            this.WriteEncodedChar('\0', Encoding.ASCII);
        }
    }
}

