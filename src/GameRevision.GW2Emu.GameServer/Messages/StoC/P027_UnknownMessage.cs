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
    public class P027_UnknownMessage : IMessage
    {
        public short Unknown0;
        public int Unknown1;
        public byte Unknown2;
        public int Unknown3;
        public byte[] Unknown4;
        public Vector4 Unknown5;
        public int Unknown6;
        public Vector4 Unknown7;
        public byte Unknown8;
        public byte Unknown9;
        public byte Unknown10;
        public Vector3 Unknown11;
        public short Unknown12;
        public short Unknown13;
        public struct Struct14
        {
            public int Unknown15;
            public int Unknown16;
            public Optional<WorldPosition> Unknown17;
            
            public void Serialize(Serializer serializer)
            {
                serializer.WriteVarint(this.Unknown15);
                serializer.WriteVarint(this.Unknown16);
                serializer.Write(this.Unknown17.IsPresent ? (byte) 1 : (byte) 0);
                if (this.Unknown17.IsPresent)
                {
                    serializer.Write(this.Unknown17.Value);
                }
            }
            public void Deserialize(Deserializer deserializer) {}
        }
        public Optional<Struct14> Unknown18;
        
        public ushort Header
        {
            get
            {
                return 27;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.Write(this.Unknown0);
            serializer.WriteVarint(this.Unknown1);
            serializer.Write(this.Unknown2);
            serializer.WriteVarint(this.Unknown3);
            serializer.Write((byte)Unknown4.Length);
            for (int i = 0; i < Unknown4.Length; i++)
            {
                serializer.Write(Unknown4[i]);
            }
            serializer.Write(this.Unknown5);
            serializer.WriteVarint(this.Unknown6);
            serializer.Write(this.Unknown7);
            serializer.Write(this.Unknown8);
            serializer.Write(this.Unknown9);
            serializer.Write(this.Unknown10);
            serializer.Write(this.Unknown11);
            serializer.Write(this.Unknown12);
            serializer.Write(this.Unknown13);
            serializer.Write(this.Unknown18.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown18.IsPresent)
            {
                this.Unknown18.Value.Serialize(serializer);
            }
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
