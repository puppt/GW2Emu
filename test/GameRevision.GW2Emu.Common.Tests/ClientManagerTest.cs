using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GameRevision.GW2Emu.Common.Network;
using NUnit.Framework;

namespace GameRevision.GW2Emu.Common.Tests
{
    [TestFixture()]
    public class ClientManagerTest
    {
        private Client client;
        private byte[] buffer;
        private int dataLen;

        private bool newClient = false;
        private bool lostClient = false;
        private bool newData = false;


        [Test()]
        public void TestCase()
        {
            // create a new client manager and register for
            // its new-client event. we will check if we actually get
            // the new client later on.
            // finally start the manager.
            var manager = new ClientListener(IPAddress.Any, 9999);
            manager.ClientConnected += this.OnClientConnected;
            manager.Listen();

            TestGeneral();

            TestKick();
        }


        private void TestGeneral()
        {
            Reset();

            // connect, send data, check data, disconnect
            using (var testClient = new TcpClient("localhost", 9999)) 
            {
                SpinWait.SpinUntil(() => newClient, 100);

                Assert.IsNotNull(client);

                var stream = testClient.GetStream();
                stream.WriteByte(0xFF);

                SpinWait.SpinUntil(() => newData, 100);

                Assert.AreEqual(1, dataLen);
                Assert.AreEqual(0xFF, buffer[0]);

                //testClient.Close();
            }

            Assert.That(SpinWait.SpinUntil(() => lostClient, 100), Is.True);
        }


        private void TestKick()
        {
            Reset();

            // connect, kick
            using (var testClient = new TcpClient("localhost", 9999)) 
            {
                SpinWait.SpinUntil(() => newClient, 100);

                Assert.IsNotNull(client);

                client.Disconnect();

                Assert.That(SpinWait.SpinUntil(() => lostClient, 100), Is.True);
            }
        }


        private void Reset()
        {
            client = null;
            buffer = new byte[]{};
            dataLen = 0;
            newClient = false;
            lostClient = false;
            newData = false;
        }


        private void OnClientConnected(Object sender, ClientConnectedEventArgs e) 
        { 
            newClient = true;

            client = e.Client;

            client.DataReceived += OnDataReceived;
            client.Disconnected += OnClientDisconnected;
        }


        private void OnDataReceived(Object sender, DataReceivedEventArgs e) 
        { 
            newData = true; 

            buffer = e.Buffer;
        }


        private void OnClientDisconnected(Object sender, ClientDisconnectedEventArgs e) 
        { 
            lostClient = true; 
        }
    }
}

