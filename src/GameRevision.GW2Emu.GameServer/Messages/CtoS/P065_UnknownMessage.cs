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
    public class P065_UnknownMessage : IMessage
    {
        public byte[] Unknown0;
        
        public ushort Header
        {
            get
            {
                return 65;
            }
        }
        
        public ISession Owner {  get;  set; }
        
        public void Serialize(Serializer serializer) {}
        public void Deserialize(Deserializer deserializer)
        {
            ushort unknown0Length = deserializer.ReadUInt16();
            if (unknown0Length > 8066)
            {
                throw new InvalidDataException();
            }
            Unknown0 = new byte[unknown0Length];
            for (int i = 0; i < Unknown0.Length; i++)
            {
                Unknown0[i] = deserializer.ReadByte();
            }
        }
    }
}
