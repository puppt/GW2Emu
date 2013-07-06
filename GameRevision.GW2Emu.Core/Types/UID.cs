using System;

namespace GameRevision.GW2Emu.Core.Types
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

            this.Data = data;
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += BytesToHex(true, true, this.Data[0], this.Data[1], this.Data[2], this.Data[3]);
            result += BytesToHex(true, true, this.Data[4], this.Data[5]);
            result += BytesToHex(true, true, this.Data[6], this.Data[7]);
            result += BytesToHex(false, true, this.Data[8], this.Data[9]);
            result += BytesToHex(false, false, this.Data[10], this.Data[11], this.Data[12], this.Data[13], this.Data[14], this.Data[15]);
            return result;
        }

        private string BytesToHex(bool reverse, bool endDash, params byte[] input)
        {
            if (reverse)
            {
                Array.Reverse(input);
            }

            return BitConverter.ToString(input).Replace("-", string.Empty) + (endDash ? "-" : "");
        }

        public static UID New()
        {
            byte[] bytes = new byte[16];
            new Random(DateTime.Now.Millisecond).NextBytes(bytes);
            return new UID(bytes);
        }
    }
}
