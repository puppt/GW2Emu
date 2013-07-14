using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using GW2Emu.Core;

namespace GameRevision.GW2Emu.Network
{
    public sealed class ClientManager
    {
        public event EventHandler<NewClientEventArgs> OnNewClient;
        public event EventHandler<LostClientEventArgs> OnLostClient;
        public event EventHandler<NewDataEventArgs> OnNewData;

        public IPEndPoint EndPoint { get; private set; }

        private const int BACKLOG = 100;
        private ICollection<Client> clients;
        private Socket socket;

        private bool running = false;


        public ClientManager(IPAddress address, int port)
        {
            EndPoint = new IPEndPoint(address, port);

            clients = new ConcurrentBag<Client>();

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(EndPoint);
        }


        public void Update()
        {
            socket.Listen(BACKLOG);

            if (IsPending())
            {
                var newClient = new Client(socket.Accept());

                clients.Add(newClient);
                OnNewClient(newClient);
            }

            Parallel.ForEach(clients, client => 
            {
                client.Update();
            });
        }


        public void Start()
        {
            running = true;
            ParallelUtils.While(() => (running), Update());
        }


        public void Stop()
        {
            running = false;
        }


        internal virtual void OnNewClient(Client client)
        {
            OnNewClient(this, new NewClientEventArgs(client));
        }


        internal virtual void OnLostClient(Client client)
        {
            OnNewClient(this, new LostClientEventArgs(client));
            
            clients.Remove(client);
        }


        internal virtual void OnNewData(Client client)
        {
            OnNewClient(this, new NewClientEventArgs(client));
        }


        private bool IsPending()
        {
            return socket.Poll(0, SelectMode.SelectRead);
        }
    }
}
