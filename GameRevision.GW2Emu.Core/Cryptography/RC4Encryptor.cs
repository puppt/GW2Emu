using System;

namespace GameRevision.GW2Emu.Core.Cryptography
{
    public class RC4Encryptor
    {
        private RC4 rc4In;
        private RC4 rc4Out;

        public RC4Encryptor(byte[] key)
        {
            this.rc4In = new RC4(key);
            this.rc4Out = new RC4(key);
        }

        public byte[] Encrypt(byte[] buffer)
        {
            return this.rc4Out.Process(buffer);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return this.rc4In.Process(buffer);
        }
    }
}
