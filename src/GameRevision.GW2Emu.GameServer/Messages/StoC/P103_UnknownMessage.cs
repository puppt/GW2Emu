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
    public class P103_UnknownMessage : IMessage
    {
        public int Unknown0;
        public float Unknown1;
        public byte Unknown2;
        public byte Unknown3;
        public int Unknown4;
        public int Unknown5;
        
        public ushort Header
        {
            get
            {
                return 103;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.WriteVarint(this.Unknown0);
            serializer.Write(this.Unknown1);
            serializer.Write(this.Unknown2);
            serializer.Write(this.Unknown3);
            serializer.WriteVarint(this.Unknown4);
            serializer.WriteVarint(this.Unknown5);
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
