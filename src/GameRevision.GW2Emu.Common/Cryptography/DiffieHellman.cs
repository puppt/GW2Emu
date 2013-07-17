using System;
using Mono.Math;

namespace GameRevision.GW2Emu.Common.Cryptography
{
    public static class DiffieHellman
    {

        public static byte[] GenerateSharedKey(byte[] publicKey, byte[] privateKey, byte[] prime)
        {
            byte[] publicKeyReversed = new byte[publicKey.Length];

            Array.Copy(publicKey, publicKeyReversed, publicKeyReversed.Length);
            Array.Reverse(publicKeyReversed);

            BigInteger a = new BigInteger(publicKeyReversed);
            BigInteger x = new BigInteger(privateKey);
            BigInteger p = new BigInteger(prime);
            BigInteger b = a.ModPow(x, p);

            byte[] sharedKey = b.GetBytes();
            Array.Reverse(sharedKey);

            return sharedKey;
        }
    }
}
