using System;
using GameRevision.GW2Emu.Core.Serializers;

namespace GameRevision.GW2Emu.Core
{
    public class GenericMessage : IMessage
    {
        //public ISession Session { get; set; }

        public virtual ushort Header
        {
            get
            {
                return 0;
            }
        }

        public virtual void Deserialize(Deserializer deserializer)
        {
        }

        public virtual void Serialize(Serializer serializer)
        {
            serializer.Write(this.Header);
        }
    }
}
