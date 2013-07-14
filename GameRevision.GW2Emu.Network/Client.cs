using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GameRevision.GW2Emu.Core;

namespace GameRevision.GW2Emu.Network
{
    public sealed class Client
    {
        public IPEndPoint RemoteEndPoint { get; private set; }
        public IPEndPoint LocalEndPoint { get; private set; }

        private ClientManager clientMan;
        private Socket socket;

        private bool running = false;


        public Client(ClientManager clientMan, Socket socket)
        {
            this.clientMan = clientMan;
            this.socket = socket;

            RemoteEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            LocalEndPoint = (IPEndPoint)socket.LocalEndPoint;
        }


        public void Send(byte[] data)
        {
            socket.Send(data);
        }


        public void Invalidate()
        {
            Stop();
            clientMan.OnLostClient(this);
        }


        internal void Start()
        {
            running = true;
            ParallelUtils.While(() => (running), Update());
        }


        internal void Stop()
        {
            running = false;
            socket.Close();
        }


        private void Update()
        {
            if (!IsConnected())
            {
                Invalidate();
                return;
            }

            if (socket.Available > 0)
            {
                var buffer = new byte[socket.Available];
                var dataLen = socket.Receive(buffer);

                clientMan.OnNewData(buffer, dataLen);
            }
        }


        private bool IsConnected()
        {
            return (socket.Connected || socket.Poll(1, SelectMode.SelectRead | SelectMode.SelectWrite));
        }
    }
}
