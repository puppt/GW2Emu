using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GameRevision.GW2Emu.Core;

namespace GameRevision.GW2Emu.Network
{
    public class NetworkSessionListener : INetworkSessionListener
    {
        public const int Backlog = 100;
        public event System.EventHandler<NetworkSessionCreatedEventArgs> NetworkSessionCreated;
        public IPEndPoint EndPoint { get; private set; }

        public bool Listening
        {
            get
            {
                return this.listening;
            }
        }

        private Socket socket;
        private volatile bool listening;

        public NetworkSessionListener(IPAddress address, int port)
        {
            this.EndPoint = new IPEndPoint(address, port);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Bind(this.EndPoint);
        }

        protected virtual bool IsPending()
        {
            return socket.Poll(0, SelectMode.SelectRead);
        }

        protected virtual void FreeWaitingThreads()
        {
            Thread.Sleep(0);
        }

        protected virtual void OnClientConnected(INetworkSession networkSession, DateTime connectionTime)
        {
            if (this.NetworkSessionCreated != null)
            {
                this.NetworkSessionCreated(this, new NetworkSessionCreatedEventArgs(networkSession, connectionTime));
            }
        }

        public void Listen()
        {
            this.listening = true;

            ThreadStart threadStart = new ThreadStart(delegate
                {
                    this.socket.Listen(Backlog);

                    while (this.listening)
                    {
                        if (this.IsPending())
                        {
                            this.OnClientConnected(new NetworkSession(this.socket.Accept()), DateTime.Now);
                        }

                        this.FreeWaitingThreads();
                    }
                });

            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        public void Stop()
        {
            this.listening = false;
        }
    }
}
