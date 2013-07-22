using System;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Messaging;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common.Events;

namespace GameRevision.GW2Emu.Common.Session
{
    public abstract class GenericSession : ISession
    {

        public ISessionState State { private get; set; }

        private Client client;
        private IEventAggregator aggregator;
        private Object deserializationLock = new Object();
        private Object serializationLock = new Object();


        protected GenericSession(Client client, IEventAggregator aggregator)
        {
            this.client = client;
            this.aggregator = aggregator;

            State = new InvalidState();
        }


        public void Send(IMessage message)
        { 
            lock (serializationLock)
            {
                // ensure that serialization & sending the messages
                // is done without disturbances by parallel calls of this
                Send(State.Serialize(this, message));
            }
        }


        /// <summary>
        /// Send the specified data. (Used internally!)
        /// Careful with using this directly, as no filtering is done
        /// and data is just passed through.
        /// </summary>
        public void Send(byte[] data)
        { 
            client.Send(data);
        }


        public void Kick()
        {
            client.Kick();

            State = new InvalidState();
        }


        public void NewDataHandler(object sender, NewDataEventArgs e)
        {
            var messages = (IEnumerable<IMessage>) new IMessage[] {};

            lock (deserializationLock)
            {
                // ensure that the deserialization can run through and change states if necessary,
                // before any more data can be recieved and deserialized
                messages = State.Deserialize(this, e.Buffer);
            }

            foreach (var message in messages) 
            {
                // call the template method
                Trigger(message);
            }
        }


        public void LostClientHandler(object sender, LostClientEventArgs e)
        {
            Trigger(new ClientDisconnectedEvent(this));
        }


        protected void Trigger(IEvent evt)
        {
            aggregator.Trigger(evt);
        }
    }
}

