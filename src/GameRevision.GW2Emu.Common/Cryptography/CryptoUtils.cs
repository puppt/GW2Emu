using System;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.Common.Cryptography
{
    public static class CryptoUtils
    {

        public static byte[] GetRandomBytes()
        {
            byte[] buffer = new byte[20];
            new Random().NextBytes(buffer);
            return buffer;
        }


        public static byte[] ToBytes(uint[] uints)
        {
            return BinaryBuilder
                .Start()
                .Write(uints[0])
                .Write(uints[1])
                .Write(uints[2])
                .Write(uints[3])
                .Write(uints[4])
                .Finish();
        }


        public static uint[] ToUints(byte[] bytes)
        {
            Deserializer deserializer = new Deserializer(bytes);
            uint[] uints = new uint[5];
            uints[0] = deserializer.ReadUInt32();
            uints[1] = deserializer.ReadUInt32();
            uints[2] = deserializer.ReadUInt32();
            uints[3] = deserializer.ReadUInt32();
            uints[4] = deserializer.ReadUInt32();
            return uints;
        }


        private static uint ROL(uint value, uint x)
        {
            return value << (int)x | value >> (int)(32 - x);
        }


        public static byte[] Hash(byte[] buffer)
        {
            uint[] state = ToUints(buffer);
            uint eax, ebx, ecx, edx, edi;

            eax = state[0];
            edx = state[1];
            eax += 0x9FB498B3U;
            ecx = eax;
            ecx = ROL(ecx, 5);
            ecx = ecx + edx + 0x66B0CD0DU;
            edx = ecx;
            edx = ROL(edx, 5);
            edx += state[2];
            edi = eax;
            edi &= 0x22222222U;
            edi = ~edi;
            edi &= 0x7BF36AE2U;
            eax = ROL(eax, 30);
            edx = edi + edx + 0xF33D5697U;
            edi = edx;
            edi = ROL(edi, 5);
            edi += state[3];
            ebx = eax;
            ebx ^= 0x59D148C0U;
            ebx &= ecx;
            ebx ^= 0x59D148C0U;
            edi = ebx + edi + 0xD675E47BU;
            ecx = ROL(ecx, 0x1e);
            ebx = eax;
            ebx ^= ecx;
            ebx &= edx;
            ebx ^= eax;
            ebx += state[4];
            state[4] += eax;
            ebx += state[0];
            eax = ecx;
            ecx = state[1];
            ecx += edi;
            edi = ROL(edi, 5);
            edx = ROL(edx, 0x1e);
            state[2] += edx;
            edx = eax;
            state[3] += edx;
            edi = ebx + edi + 0xB453C259U;
            state[0] = edi;
            state[1] = ecx;

            return ToBytes(state);
        }


        public static byte[] XOR(byte[] value, byte[] key)
        {
            byte[] buffer = new byte[20];

            for (int i = 0; i < 20; i++)
            {
                buffer[i] = (byte)(value[i] ^ key[i]);
            }

            return buffer;
        }
    }
}
