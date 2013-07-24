using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameRevision.GW2Emu.Common.Network
{
    public sealed class Client
    {
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<ClientDisconnectedEventArgs> Disconnected;

        public IPEndPoint RemoteEndPoint { get; private set; }
        public IPEndPoint LocalEndPoint { get; private set; }

        private Socket socket;
        private volatile bool running;

        public Client(Socket socket)
        {
            this.socket = socket;

            this.RemoteEndPoint = (IPEndPoint)this.socket.RemoteEndPoint;
            this.LocalEndPoint = (IPEndPoint)this.socket.LocalEndPoint;
        }

        private bool IsRunning()
        {
            return this.running;
        }

        private bool IsConnected()
        {
            return (this.socket.Connected || socket.Poll(1, SelectMode.SelectRead | SelectMode.SelectWrite));
        }

        private void FreeWaitingThreads()
        {
            Thread.Sleep(0);
        }

        private void OnDataReceived(byte[] buffer, int bytesRead)
        {
            if (this.DataReceived != null)
            {
                this.DataReceived(this, new DataReceivedEventArgs(buffer));
            }
        }

        private void OnDisconnected()
        {
            if (this.Disconnected != null)
            {
                this.Disconnected(this, new ClientDisconnectedEventArgs());
            }
        }

        public void Run()
        {
            this.running = true;

            ParallelUtils.While(this.IsRunning, delegate
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
                    this.Disconnect();
                }

                //this.FreeWaitingThreads();
            });
        }

        public void Send(byte[] data)
        {
            this.socket.Send(data);
        }

        public void Disconnect()
        {
            this.running = false;
            this.socket.Close();
            this.OnDisconnected();
        }
    }
}
