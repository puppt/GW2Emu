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

namespace GameRevision.GW2Emu.GameServer.Messages.CtoS
{
    public class P022_UnknownMessage : IMessage
    {
        public short Unknown0;
        public short Unknown1;
        public int Unknown2;
        public byte Unknown3;
        
        public ushort Header
        {
            get
            {
                return 22;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer) {}
        public void Deserialize(Deserializer deserializer)
        {
            this.Unknown0 = deserializer.ReadInt16();
            this.Unknown1 = deserializer.ReadInt16();
            this.Unknown2 = deserializer.ReadVarint();
            this.Unknown3 = deserializer.ReadByte();
        }
    }
}
