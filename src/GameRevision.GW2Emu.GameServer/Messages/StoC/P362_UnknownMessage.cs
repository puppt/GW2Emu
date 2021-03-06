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
    public class P362_UnknownMessage : IMessage
    {
        public int Unknown0;
        public byte Unknown1;
        public string Unknown2;
        public Vector3 Unknown3;
        public byte Unknown4;
        public string Unknown5;
        public string Unknown6;
        
        public ushort Header
        {
            get
            {
                return 362;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.WriteVarint(this.Unknown0);
            serializer.Write(this.Unknown1);
            serializer.WriteUtf16String(this.Unknown2);
            serializer.Write(this.Unknown3);
            serializer.Write(this.Unknown4);
            serializer.WriteUtf16String(this.Unknown5);
            serializer.WriteUtf16String(this.Unknown6);
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
