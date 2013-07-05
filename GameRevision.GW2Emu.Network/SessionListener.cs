using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GameRevision.GW2Emu.Core;
using GameRevision.GW2Emu.Core.Generic;
using GameRevision.GW2Emu.Core.EventDesign;

namespace GameRevision.GW2Emu.Network
{
    public class SessionListener : ISessionListener
    {
        public const int Backlog = 100;
        public event SessionCreatedEventHandler SessionCreated;
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

        public SessionListener(IPAddress address, int port)
        {
            this.EndPoint = new IPEndPoint(address, port);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Bind(this.EndPoint);
        }

        protected bool IsPending()
        {
            return socket.Poll(0, SelectMode.SelectRead);
        }

        protected void FreeWaitingThreads()
        {
            Thread.Sleep(0);
        }

        protected void OnSessionCreated(ISession session, DateTime creationTime)
        {
            if (this.SessionCreated != null)
            {
                this.SessionCreated(this, new SessionCreatedEventArgs(session, creationTime));
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
                            INetworkSession networkSession = new NetworkSession(this.socket.Accept());
                            ISession session = new GenericSession(networkSession);
                            this.OnSessionCreated(session, DateTime.Now);
                        }

                        this.FreeWaitingThreads();
                    }
                });

            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        public void Dispose()
        {
            this.listening = false;
        }
    }
}
