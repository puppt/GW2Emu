using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameRevision.GW2Emu.Common.Network
{
    public sealed class ClientListener
    {
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public IPEndPoint EndPoint { get; private set; }

        private const int Backlog = 100;

        private Socket socket;
        private volatile bool listening;

        public ClientListener(IPAddress address, int port)
        {
            this.EndPoint = new IPEndPoint(address, port);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Bind(this.EndPoint);
        }

        private bool IsListening()
        {
            return this.listening;
        }

        private bool IsPending()
        {
            return socket.Poll(0, SelectMode.SelectRead);
        }

        private void FreeWaitingThreads()
        {
            Thread.Sleep(0);
        }

        private void OnClientConnected(Client client)
        {
            if (this.ClientConnected != null)
            {
                this.ClientConnected(this, new ClientConnectedEventArgs(client));
            }
        }

        public void Listen()
        {
            this.listening = true;

            this.socket.Listen(Backlog);

            ParallelUtils.While(this.IsListening, delegate
            {
                if (this.IsPending())
                {
                    this.OnClientConnected(new Client(this.socket.Accept()));
                }

                //this.FreeWaitingThreads();
            });
        }

        public void Stop()
        {
            this.listening = false;
            this.socket.Close();
        }
    }
}

