using System;
using GameRevision.GW2Emu.Core.Serializers;

namespace GameRevision.GW2Emu.Core
{
    public interface IMessage
    {
        ISession Session { get; set;  }
        ushort Header { get; }
        void Deserialize(Deserializer deserializer);
        void Serialize(Serializer serializer);
    }
}
