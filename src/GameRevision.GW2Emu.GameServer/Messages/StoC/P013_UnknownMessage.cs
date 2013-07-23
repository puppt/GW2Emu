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
    public class P013_UnknownMessage : IMessage
    {
        public short Unknown0;
        public int Unknown1;
        public byte Unknown2;
        public int Unknown3;
        public byte[] Unknown4;
        public byte Unknown5;
        public int Unknown6;
        public byte Unknown7;
        public float Unknown8;
        public WorldPosition Unknown9;
        
        public ushort Header
        {
            get
            {
                return 13;
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
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
