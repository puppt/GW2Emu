using System;
using System.IO;
using System.Net;
using System.Text;
using GameRevision.GW2Emu.Core.Types;

namespace GameRevision.GW2Emu.Core.Serializers
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

        public void WriteVector2(Vector2 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
        }

        public void WriteVector3(Vector3 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
            this.Write(value.Z);
        }

        public void WriteVector4(Vector4 value)
        {
            this.Write(value.X);
            this.Write(value.Y);
            this.Write(value.Z);
            this.Write(value.W);
        }

        public void WriteWorldPosition(WorldPosition value)
        {
            this.WriteVector3(value.Vector);
            this.WriteVarint(value.W);
        }

        public void WriteUID(UID value)
        {
            this.Write(value.Data);
        }
        public void WriteIPEndPoint(IPEndPoint value)
        {
            SocketAddress socketAddress = value.Serialize();
            byte[] socketAddressBytes = new byte[28];

            for (int i = 0; i < socketAddress.Size; i++)
            {
                socketAddressBytes[i] = socketAddress[i];
            }

            this.Write(socketAddressBytes);
        }
    }
}
