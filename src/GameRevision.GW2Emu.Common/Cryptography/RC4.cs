using System;

namespace GameRevision.GW2Emu.Common.Cryptography
{
    public class RC4
    {

        private byte[] sbox = new byte[256];
        private int i = 0;
        private int j = 0;

        public readonly byte[] Key;


        public RC4(byte[] key)
        {
            Key = key;

            int length = key.Length;

            for (int i = 0; i < 256; i++)
            {
                sbox[i] = (byte)i;
            }

            int k = 0;

            for (int i = 0; i < 256; i++)
            {
                k = (k + sbox[i] + key[i % length]) % 256;
                Swap<byte>(ref sbox[i], ref sbox[k]);
            }
        }


        public byte[] Process(byte[] value)
        {
            int length = value.Length;
            byte[] tmp = new byte[length];

            for (int n = 0; n < length; n++)
            {
                i = ++i % 256;
                j = (j + sbox[i]) % 256;
                Swap<byte>(ref sbox[i], ref sbox[j]);
                byte rand = sbox[(sbox[i] + sbox[j]) % 256];
                tmp[n] = (byte)(rand ^ value[n]);
            }

            return tmp;
        }


        private void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
    }
}
