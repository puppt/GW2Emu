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
    public class P246_UnknownMessage : IMessage
    {
        public int[] Unknown0;
        
        public ushort Header
        {
            get
            {
                return 246;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.Write((ushort)Unknown0.Length);
            for (int i = 0; i < Unknown0.Length; i++)
            {
                serializer.WriteVarint(Unknown0[i]);
            }
        }
        public void Deserialize(Deserializer deserializer) {}
    }
}
