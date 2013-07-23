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
    public class P457_UnknownMessage : IMessage
    {
        public int Unknown0;
        public int Unknown1;
        public int Unknown2;
        public byte Unknown3;
        public byte Unknown4;
        public int Unknown5;
        public int Unknown6;
        public string Unknown7;
        public WorldPosition Unknown8;
        public byte Unknown9;
        public float Unknown10;
        public int Unknown11;
        
        public ushort Header
        {
            get
            {
                return 457;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.WriteVarint(this.Unknown0);
            serializer.WriteVarint(this.Unknown1);
            serializer.WriteVarint(this.Unknown2);
            serializer.Write(this.Unknown3);
            serializer.Write(this.Unknown4);
            serializer.WriteVarint(this.Unknown5);
            serializer.WriteVarint(this.Unknown6);
            serializer.WriteUtf16String(this.Unknown7);
            serializer.Write(this.Unknown8);
            serializer.Write(this.Unknown9);
            serializer.Write(this.Unknown10);
            serializer.WriteVarint(this.Unknown11);
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
