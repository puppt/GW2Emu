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
    public class P106_UnknownMessage : IMessage
    {
        public int Unknown0;
        public byte Unknown1;
        public short[] Unknown2;
        
        public ushort Header
        {
            get
            {
                return 106;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer) {}
        public void Deserialize(Deserializer deserializer)
        {
            this.Unknown0 = deserializer.ReadVarint();
            this.Unknown1 = deserializer.ReadByte();
            byte unknown2Length = deserializer.ReadByte();
            if (unknown2Length > 21)
            {
                throw new InvalidDataException();
            }
            Unknown2 = new short[unknown2Length];
            for (int i = 0; i < Unknown2.Length; i++)
            {
                Unknown2[i] = deserializer.ReadInt16();
            }
        }
    }
}
