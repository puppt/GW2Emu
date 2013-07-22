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
    public class P295_UnknownMessage : IMessage
    {
        public int Unknown0;
        public int[] Unknown1;
        public int[] Unknown2;
        public byte[] Unknown3;
        
        public ushort Header
        {
            get
            {
                return 295;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.WriteVarint(this.Unknown0);
            serializer.Write((byte)Unknown1.Length);
            for (int i = 0; i < Unknown1.Length; i++)
            {
                serializer.WriteVarint(Unknown1[i]);
            }
            serializer.Write((byte)Unknown2.Length);
            for (int i = 0; i < Unknown2.Length; i++)
            {
                serializer.WriteVarint(Unknown2[i]);
            }
            serializer.Write((byte)Unknown3.Length);
            for (int i = 0; i < Unknown3.Length; i++)
            {
                serializer.Write(Unknown3[i]);
            }
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
