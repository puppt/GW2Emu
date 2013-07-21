using System;
using GameRevision.GW2Emu.Common.Events;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.Common.Messaging
{
    public interface IMessage : IEvent
    {
        ushort Header { get; set; }

        void Serialize(Serializer serializer);
        void Deserialize(Deserializer deserializer);
    }
}
