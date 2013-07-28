using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NLog;

namespace GameRevision.GW2Emu.Common.Network
{
    public sealed class ClientListener
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public IPEndPoint EndPoint { get; private set; }

        public bool Listening
        {
            get
            {
                return this.listening;
            }
        }

        private const int Backlog = 100;

        private event Action Stopped;
        private Socket socket;
        private volatile bool listening;

        public ClientListener(IPAddress address, int port)
        {
            this.EndPoint = new IPEndPoint(address, port);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Bind(this.EndPoint);
        }

        private bool IsPending()
        {
            return socket.Poll(0, SelectMode.SelectRead);
        }

        private void FreeWaitingThreads()
        {
            Thread.Sleep(1);
        }

        private void OnClientConnected(Client client)
        {
            if (this.ClientConnected != null)
            {
                this.ClientConnected(this, new ClientConnectedEventArgs(client));
            }
        }

        private void OnStopped()
        {
            if (this.Stopped != null)
            {
                this.Stopped();
            }
        }

        public void Listen()
        {
            this.listening = true;

            this.socket.Listen(Backlog);

            ThreadStart listen = delegate
            {
                while (this.listening)
                {
                    if (this.IsPending())
                    {
                        Client client = new Client(this.socket.Accept());

                        this.Stopped += delegate
                        {
                            client.Disconnect();
                        };

                        logger.Debug("Client connected.");

                        this.OnClientConnected(client);
                    }

                    this.FreeWaitingThreads();
                }
            };

            Thread thread = new Thread(listen);
            thread.IsBackground = false;
            thread.Start();
        }

        public void Stop()
        {
            this.listening = false;

            this.socket.Close();
            this.OnStopped();
        }
    }
}

