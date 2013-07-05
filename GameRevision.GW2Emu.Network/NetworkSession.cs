using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GameRevision.GW2Emu.Core;
using GameRevision.GW2Emu.Core.EventDesign;

namespace GameRevision.GW2Emu.Network
{
    public class NetworkSession : INetworkSession
    {
        public event DataReceivedEventHandler DataReceived;
        public ISession Parent { get; private set; }
        public IPEndPoint RemoteEndPoint { get; private set; }
        public IPEndPoint LocalEndPoint { get; private set; }

        public bool Running
        {
            get
            {
                return this.running;
            }
        }

        private Socket socket;
        private volatile bool running;

        public NetworkSession(Socket socket)
        {
            this.socket = socket;
            this.RemoteEndPoint = (IPEndPoint)this.socket.RemoteEndPoint;
            this.LocalEndPoint = (IPEndPoint)this.socket.LocalEndPoint;
        }

        protected bool IsConnected()
        {
            return (this.socket.Connected || socket.Poll(1, SelectMode.SelectRead | SelectMode.SelectWrite));
        }

        protected void FreeWaitingThreads()
        {
            Thread.Sleep(0);
        }

        protected void OnDataReceived(byte[] buffer, int bytesRead)
        {
            if (this.DataReceived != null)
            {
                this.DataReceived(this, new DataReceivedEventArgs(buffer, bytesRead));
            }
        }

        public void Run()
        {
            this.running = true;

            ThreadStart threadStart = new ThreadStart(delegate
                {
                    while (this.running)
                    {
                        if (this.IsConnected())
                        {
                            if (this.socket.Available > 0)
                            {
                                byte[] buffer = new byte[this.socket.Available];
                                int bytesRead = this.socket.Receive(buffer);
                                this.OnDataReceived(buffer, bytesRead);
                            }
                        }
                        else
                        {
                            this.Stop();
                        }

                        this.FreeWaitingThreads();
                    }
                });

            Thread thread = new Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();
        }

        public void BindToSession(ISession parent)
        {
            this.Parent = parent;
        }

        public void Stop()
        {
            this.running = false;
        }
    }
}
