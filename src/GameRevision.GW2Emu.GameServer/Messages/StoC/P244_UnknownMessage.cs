/*
 * This code was autogenerated by
 * GameRevision.GW2Emu.CodeWriter.
 * Date generated: 24-07-13
 */

using System;
using System.IO;
using System.Net;
using GameRevision.GW2Emu.Common;
using GameRevision.GW2Emu.Common.Math;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Messaging;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.GameServer.Messages.StoC
{
    public class P244_UnknownMessage : GenericMessage
    {
        public int Unknown0;
        public byte Unknown1;
        public int Unknown2;
        public int Unknown3;
        public int Unknown4;
        public int Unknown5;
        public int Unknown6;
        public struct Struct7
        {
            public int Unknown8;
            public byte Unknown9;
            
            public void Serialize(Serializer serializer)
            {
                serializer.WriteVarint(this.Unknown8);
                serializer.Write(this.Unknown9);
            }
        }
        public Struct7[] Unknown10;
        public Optional<int> Unknown11;
        public Optional<byte> Unknown12;
        public Optional<byte> Unknown13;
        public Optional<int> Unknown14;
        public Optional<byte> Unknown15;
        public struct Struct16
        {
            public UID Unknown17;
            public byte Unknown18;
            public int Unknown19;
            public int Unknown20;
            public long Unknown21;
            
            public void Serialize(Serializer serializer)
            {
                serializer.Write(this.Unknown17);
                serializer.Write(this.Unknown18);
                serializer.WriteVarint(this.Unknown19);
                serializer.WriteVarint(this.Unknown20);
                serializer.Write(this.Unknown21);
            }
        }
        public Optional<Struct16> Unknown22;
        public Optional<int> Unknown23;
        public Optional<int> Unknown24;
        
        public override ushort Header
        {
            get
            {
                return 244;
            }
        }
        
        public override void Serialize(Serializer serializer)
        {
            serializer.Write(Header);
            serializer.WriteVarint(this.Unknown0);
            serializer.Write(this.Unknown1);
            serializer.WriteVarint(this.Unknown2);
            serializer.WriteVarint(this.Unknown3);
            serializer.WriteVarint(this.Unknown4);
            serializer.WriteVarint(this.Unknown5);
            serializer.WriteVarint(this.Unknown6);
            serializer.Write((byte)Unknown10.Length);
            for (int i = 0; i < Unknown10.Length; i++)
            {
                Unknown10[i].Serialize(serializer);
            }
            serializer.Write(this.Unknown11.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown11.IsPresent)
            {
                serializer.WriteVarint(this.Unknown11.Value);
            }
            serializer.Write(this.Unknown12.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown12.IsPresent)
            {
                serializer.Write(this.Unknown12.Value);
            }
            serializer.Write(this.Unknown13.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown13.IsPresent)
            {
                serializer.Write(this.Unknown13.Value);
            }
            serializer.Write(this.Unknown14.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown14.IsPresent)
            {
                serializer.WriteVarint(this.Unknown14.Value);
            }
            serializer.Write(this.Unknown15.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown15.IsPresent)
            {
                serializer.Write(this.Unknown15.Value);
            }
            serializer.Write(this.Unknown22.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown22.IsPresent)
            {
                this.Unknown22.Value.Serialize(serializer);
            }
            serializer.Write(this.Unknown23.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown23.IsPresent)
            {
                serializer.WriteVarint(this.Unknown23.Value);
            }
            serializer.Write(this.Unknown24.IsPresent ? (byte) 1 : (byte) 0);
            if (this.Unknown24.IsPresent)
            {
                serializer.WriteVarint(this.Unknown24.Value);
            }
        }
    }
}