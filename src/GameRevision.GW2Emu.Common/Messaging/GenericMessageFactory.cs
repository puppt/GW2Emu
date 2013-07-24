using System;
using GameRevision.GW2Emu.Common.Serialization;
using System.Collections.Generic;

namespace GameRevision.GW2Emu.Common.Messaging
{
    public abstract class GenericMessageFactory : IMessageFactory
    {
        public IEnumerable<IMessage> CreateMessages(byte[] data)
        {
            var deserializer = new Deserializer(data);

            while (!deserializer.EndOfStream)
            {
                // template method call
                IMessage message = this.CreateEmptyMessage(deserializer.ReadUInt16());

                // TODO: implement stream chopping and stream combination here somhow, or in the Deserializer

                // check if the data is enough, in that case return and save the data in between or something

                message.Deserialize(deserializer);

                yield return message;
            }
        }

        protected abstract IMessage CreateEmptyMessage(ushort header);
    }
}

