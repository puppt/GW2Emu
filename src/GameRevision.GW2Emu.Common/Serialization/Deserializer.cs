using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common.Math;

namespace GameRevision.GW2Emu.Common.Serialization
{
    public class Deserializer : BinaryReader
    {
        public Deserializer(byte[] bytes) : base(new MemoryStream(bytes))
        {
        }

        public bool EndOfStream
        {
            get
            {
                return this.BaseStream.Position >= this.BaseStream.Length;
            }
        }

        /*
        public bool IsEmpty()
        {
            return base.BaseStream.Position >= base.BaseStream.Length -1;
        }
        */

        public int ReadVarint()
        {
            bool more = true;
            int value = 0;
            int shift = 0;

            while (more)
            {
                byte lower7bits = ReadByte ();
                more = (lower7bits & 128) != 0;
                value |= (lower7bits & 0x7f) << shift;
                shift += 7;
            }

            return value;
        }

        public long ReadVarbyte(int count)
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(ReadBytes(count));
            buffer.AddRange(new byte[8 - buffer.Count]);
            return BitConverter.ToInt64(buffer.ToArray(), 0);
        }

        public char ReadEncodedChar(Encoding encoding)
        {
            int size = encoding.IsSingleByte ? 1 : 2;
            byte[] buffer = ReadBytes (size);
            return encoding.GetChars(buffer)[0];
        }

        public string ReadUtf16String()
        {
            string text = string.Empty;
            char character= ReadEncodedChar (Encoding.Unicode);

            while (character != '\0')
            {
                text += character;
                character = ReadEncodedChar (Encoding.Unicode);
            }

            return text;
        }

        public string ReadAsciiString()
        {
            string text = string.Empty;
            char character= ReadEncodedChar(Encoding.ASCII);

            while (character != '\0')
            {
                text += character;
                character = ReadEncodedChar(Encoding.ASCII);
            }

            return text;
        }

        public Vector2 ReadVector2()
        {
            float x = ReadSingle ();
            float y = ReadSingle();
            return new Vector2(x, y);
        }

        public Vector3 ReadVector3()
        {
            float x = ReadSingle();
            float y = ReadSingle();
            float z = ReadSingle();
            return new Vector3(x, y, z);
        }

        public Vector4 ReadVector4()
        {
            float x = ReadSingle();
            float y = ReadSingle();
            float z = ReadSingle();
            float w = ReadSingle();
            return new Vector4(x, y, z, w);
        }

        public WorldPosition ReadWorldPosition()
        {
            Vector3 vector = ReadVector3();
            int w = ReadInt32();
            return new WorldPosition(vector, w);
        }

        public UID ReadUID()
        {
            byte[] buffer = ReadBytes(16);
            return new UID(buffer);
        }

        public IPEndPoint ReadIPEndPoint()
        {
            SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetwork);
            byte[] socketAddressBytes = ReadBytes(28);

            for (int i = 0; i < socketAddressBytes.Length; i++)
            {
                socketAddress[i] = socketAddressBytes[i];
            }

            return (IPEndPoint)new IPEndPoint(IPAddress.Any, 0).Create(socketAddress);
        }
    }
}
