using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using GameRevision.GW2Emu.Core;

namespace GameRevision.GW2Emu.Network
{
    public sealed class ClientManager
    {
        public event System.EventHandler<NewClientEventArgs> OnNewClient;
        public event System.EventHandler<LostClientEventArgs> OnLostClient;
        public event System.EventHandler<NewDataEventArgs> OnNewData;

        public IPEndPoint EndPoint { get; private set; }

        private const int BACKLOG = 100;
        private ICollection<Client> clients = new List<Client>();
        private Socket socket;

        private bool running = false;


        public ClientManager(int port)
        {
            EndPoint = new IPEndPoint(IPAddress.Any, port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Blocking = true;
            socket.Bind(EndPoint);
        }


        public void Start()
        {
            running = true;
            ParallelUtils.While(() => (running), Update);
        }


        public void Stop()
        {
            running = false;
            clients.All((client) => { client.Stop(); return true; });
            clients.Clear();
        }


        internal void NewClient(Client client)
        {
            OnNewClient(this, new NewClientEventArgs(client));
        }


        internal void LostClient(Client client)
        {
            OnLostClient(this, new LostClientEventArgs(client));
            
            clients.Remove(client);
        }


        internal void NewData(Client client, byte[] buffer, int dataLen)
        {
            OnNewData(this, new NewDataEventArgs(client, buffer, dataLen));
        }


        private void Update()
        {
            socket.Listen(BACKLOG);

            if (IsPending())
            {
                var newClient = new Client(this, socket.Accept());

                clients.Add(newClient);
                newClient.Start();

                NewClient(newClient);
            }
        }


        private bool IsPending()
        {
            return socket.Poll(0, SelectMode.SelectRead);
        }
    }
}
