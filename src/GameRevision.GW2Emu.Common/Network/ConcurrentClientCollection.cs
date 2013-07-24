using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameRevision.GW2Emu.Common.Session;

namespace GameRevision.GW2Emu.Common.Network
{
    public class ConcurrentClientCollection
    {
        private readonly object listLock = new object();
        private List<Client> list;

        public ConcurrentClientCollection()
        {
            this.list = new List<Client>();
        }

        public void Add(Client client)
        {
            lock (this.listLock)
            {
                client.Disconnected += delegate(object sender, ClientDisconnectedEventArgs e)
                {
                    this.Remove((Client)sender);
                };

                this.list.Add(client);
            }
        }

        public void Remove(Client client)
        {
            lock (this.listLock)
            {
                this.list.Remove(client);
            }
        }

        public void StopAll()
        {
            Parallel.ForEach(this.list, delegate(Client client)
            {
                client.Disconnect();
            });
        }
    }
}
