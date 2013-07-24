using System;
using GameRevision.GW2Emu.Common.Session;
using GameRevision.GW2Emu.Common.Serialization;

namespace GameRevision.GW2Emu.Common.Messaging
{
    public abstract class GenericMessage : IMessage
    {
        public abstract ushort Header { get; }

        public ISession Session { get; set; }

        public virtual void Deserialize(Deserializer deserializer)
        {
            throw new NotImplementedException();
        }

        public virtual void Serialize(Serializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
