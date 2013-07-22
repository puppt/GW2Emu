/*
 * This code was autogenerated by
 * GameRevision.GW2Emu.CodeWriter.
 * Date generated: 22-07-13
 */

using System;
using System.IO;
using System.Net;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Math;
using GameRevision.GW2Emu.Common.Messaging;
using GameRevision.GW2Emu.Common.Serialization;
using GameRevision.GW2Emu.Common.Session;

namespace GameRevision.GW2Emu.GameServer.Messages.StoC
{
    public class P604_UnknownMessage : IMessage
    {
        public UID Unknown0;
        public int Unknown1;
        public short Unknown2;
        public short Unknown3;
        public short Unknown4;
        public short Unknown5;
        public short Unknown6;
        public byte Unknown7;
        public byte Unknown8;
        public int[] Unknown9;
        public string Unknown10;
        public string Unknown11;
        public byte Unknown12;
        public byte Unknown13;
        public struct Struct14
        {
            public byte Unknown15;
            public byte Unknown16;
            public int Unknown17;
            public int Unknown18;
            public int Unknown19;
            public int Unknown20;
            public struct Struct21
            {
                public byte Unknown22;
                public int Unknown23;
                
                public void Serialize(Serializer serializer)
                {
                    serializer.Write(this.Unknown22);
                    serializer.WriteVarint(this.Unknown23);
                }
                public void Deserialize(Deserializer deserializer) {}
            }
            public Struct21[] Unknown24;
            
            public void Serialize(Serializer serializer)
            {
                serializer.Write(this.Unknown15);
                serializer.Write(this.Unknown16);
                serializer.WriteVarint(this.Unknown17);
                serializer.WriteVarint(this.Unknown18);
                serializer.WriteVarint(this.Unknown19);
                serializer.WriteVarint(this.Unknown20);
                serializer.Write((byte)Unknown24.Length);
                for (int i = 0; i < Unknown24.Length; i++)
                {
                    Unknown24[i].Serialize(serializer);
                }
            }
            public void Deserialize(Deserializer deserializer) {}
        }
        public Struct14[] Unknown25;
        public byte Unknown26;
        public byte Unknown27;
        
        public ushort Header
        {
            get
            {
                return 604;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.Write(this.Unknown0);
            serializer.WriteVarint(this.Unknown1);
            serializer.Write(this.Unknown2);
            serializer.Write(this.Unknown3);
            serializer.Write(this.Unknown4);
            serializer.Write(this.Unknown5);
            serializer.Write(this.Unknown6);
            serializer.Write(this.Unknown7);
            serializer.Write(this.Unknown8);
            serializer.Write((byte)Unknown9.Length);
            for (int i = 0; i < Unknown9.Length; i++)
            {
                serializer.WriteVarint(Unknown9[i]);
            }
            serializer.WriteUtf16String(this.Unknown10);
            serializer.WriteUtf16String(this.Unknown11);
            serializer.Write(this.Unknown12);
            serializer.Write(this.Unknown13);
            serializer.Write((byte)Unknown25.Length);
            for (int i = 0; i < Unknown25.Length; i++)
            {
                Unknown25[i].Serialize(serializer);
            }
            serializer.Write(this.Unknown26);
            serializer.Write(this.Unknown27);
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
