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

            // template method call
            var message = this.CreateEmptyMessage((ushort) deserializer.ReadInt16());

            // TODO: implement stream chopping and stream combination here somhow, or in the Deserializer

            // check if the data is enough, in that case return and save the data in between or something

            message.Deserialize(deserializer);

            // check if we got leftover data
            // or simply return the message we just read
            if (deserializer.IsEmpty()) { return new List<IMessage> { message }; }

            // else invoke this method with the leftover data, and add the messages to the list of messages
            // maybe yield the messages one after another?
            return new List<IMessage>();
        }

        protected abstract IMessage CreateEmptyMessage(ushort header);
    }
}

