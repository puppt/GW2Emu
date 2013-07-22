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
    public class P377_UnknownMessage : IMessage
    {
        public UID Unknown0;
        public short Unknown1;
        public short Unknown2;
        public short[] Unknown3;
        public byte Unknown4;
        
        public ushort Header
        {
            get
            {
                return 377;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.Write(this.Unknown0);
            serializer.Write(this.Unknown1);
            serializer.Write(this.Unknown2);
            for (int i = 0; i < this.Unknown3.Length; i++)
            {
                serializer.Write(this.Unknown3[i]);
            }
            serializer.Write(this.Unknown4);
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
