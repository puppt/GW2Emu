using System;
using System.Net;
using System.Net.Sockets;

namespace GameRevision.GW2Emu.Common.Network
{
    /// <summary>
    /// Based on http://msdn.microsoft.com/en-us/magazine/cc300760.aspx#S5
    /// </summary>
    public sealed class ClientManager
    {
        public event EventHandler<NewClientEventArgs> OnNewClient;
        internal event Action OnShutdown;

        private const int BACKLOG = 100;
        private Socket serverSocket;


        public ClientManager(int port)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
        }


        public void Start()
        {
            serverSocket.Listen(BACKLOG);

            for (int i = 0; i < 10; i++)
            {
                serverSocket.BeginAccept(
                    new AsyncCallback(AcceptCallback), 
                    null);
            }
        }


        public void Stop()
        {
            serverSocket.Close();
            OnShutdown();
        }


        private void AcceptCallback(IAsyncResult result)
        {
            try
            {
                // accept the client and create the object
                var clientSocket = serverSocket.EndAccept(result);

                var newClient = new Client(this, clientSocket);
                newClient.StartRecieve();

                // trigger the event
                OnNewClient(this, new NewClientEventArgs(newClient));

                // restart accept
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (SocketException exc)
            {
                serverSocket.Close();
                Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                serverSocket.Close();
                Console.WriteLine("Exception: " + exc);
            }
        }
    }
}

