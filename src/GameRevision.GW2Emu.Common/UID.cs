using System;

namespace GameRevision.GW2Emu.Common
{
    public class UID
    {

        public byte[] Data { get; private set; }


        public UID(byte[] data)
        {
            if (data.Length != 16)
            {
                throw new ArgumentOutOfRangeException();
            }

            Data = data;
        }


        public static UID Random()
        {
            byte[] bytes = new byte[16];
            new Random(DateTime.Now.Millisecond).NextBytes(bytes);
            return new UID(bytes);
        }


        public override string ToString()
        {
            string result = string.Empty;
            result += BytesToHex(true, true, Data[0], Data[1], Data[2], Data[3]);
            result += BytesToHex(true, true, Data[4], Data[5]);
            result += BytesToHex(true, true, Data[6], Data[7]);
            result += BytesToHex(false, true, Data[8], Data[9]);
            result += BytesToHex(false, false, Data[10], Data[11], Data[12], Data[13], Data[14], Data[15]);
            return result;
        }


        string BytesToHex(bool reverse, bool endDash, params byte[] input)
        {
            if (reverse)
            {
                Array.Reverse(input);
            }

            return BitConverter.ToString(input).Replace("-", string.Empty) + (endDash ? "-" : "");
        }
    }
}
