using System;
using GameRevision.GW2Emu.Common.Network;
using GameRevision.GW2Emu.Common.Messaging;
using System.Collections.Generic;

namespace GameRevision.GW2Emu.Common.Session
{
    public abstract class GenericSession : ISession
    {

        public ISessionState State { private get; set; }

        Client client;
        Object deserializationLock = new Object();
        Object serializationLock = new Object();


        protected GenericSession(Client client)
        {
            this.client = client;

            State = new InvalidState();
        }


        public void OnDataReceived(object sender, NewDataEventArgs e)
        {
            var messages = (IEnumerable<IMessage>) new IMessage[] {};

            lock (deserializationLock)
            {
                // ensure that the deserialization can run through and change states if necessary,
                // before any more data can be recieved and deserialized
                messages = State.Deserialize(this, e.Buffer, e.DataLen);
            }

            foreach (var message in messages) 
            {
                // call the template method
                Trigger(message);
            }
        }

        protected abstract void Trigger(IMessage message);


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
    }
}

