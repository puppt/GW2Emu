using System;
using System.Net;
using System.Net.Sockets;

namespace GameRevision.GW2Emu.Network
{
    public sealed class Client
    {
        public event EventHandler<NewDataEventArgs> OnNewData;
        public event EventHandler<LostClientEventArgs> OnLostClient;

        public IPEndPoint RemoteEndPoint { get; private set; }
        public IPEndPoint LocalEndPoint { get; private set; }

        private Socket socket;
        private byte[] buffer = new byte[255];


        public Client(ClientManager manager, Socket socket)
        {
            this.socket = socket;

            RemoteEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            LocalEndPoint = (IPEndPoint)socket.LocalEndPoint;

            // kick this when the server is going down
            manager.OnShutdown += Kick;
        }


        public void Send(byte[] data)
        {
            socket.Send(data);
        }


        public void Kick()
        {
            socket.Close();
            OnLostClient(this, new LostClientEventArgs(this));
        }


        internal void StartRecieve()
        {
            socket.BeginReceive(
                buffer, 0, buffer.Length, 
                SocketFlags.None, 
                new AsyncCallback(ReceiveCallback), 
                null);
        }


        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                // recieve the data, do a failcheck
                int dataLen = socket.EndReceive(result);

                if (dataLen == 0)
                {
                    Kick();
                    return;
                }

                // trigger the event
                OnNewData(this, new NewDataEventArgs(this, buffer, dataLen));

                // restart recieve
                socket.BeginReceive(
                    buffer, 0, buffer.Length, 
                    SocketFlags.None, 
                    new AsyncCallback(ReceiveCallback), 
                    null);
            }
            catch (SocketException exc)
            {
                Kick();
                Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                Kick();
                Console.WriteLine("Exception: " + exc);
            }
        }
    }
}
