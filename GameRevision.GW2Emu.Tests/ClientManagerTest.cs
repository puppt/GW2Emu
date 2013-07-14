using System;
using System.Diagnostics;
using System.Net.Sockets;
using GameRevision.GW2Emu.Network;
using NUnit.Framework;

namespace GameRevision.GW2Emu.Test
{
    [TestFixture()]
    public class ClientManagerTest
    {
        private bool newClient = false;
        private bool lostClient = false;
        private bool newData = false;


        [Test()]
        public void TestCase()
        {
            // create a new client manager and register for its events
            var cliMan = new ClientManager(9999);

            cliMan.OnNewClient += OnNewClient;
            cliMan.OnLostClient += OnLostClient;
            cliMan.OnNewData += OnNewData;

            // start the client manager
            cliMan.Start();

            // connect, send data, disconnect
            using (var client = new TcpClient("localhost", 9999))
            {
                var stream = client.GetStream();
                stream.WriteByte(0xFF);
            }

            Debug.Assert(newClient);
        }


        private void OnNewClient(Object sender, NewClientEventArgs args) { newClient = true; }
        private void OnLostClient(Object sender, LostClientEventArgs args) { lostClient = true; }
        private void OnNewData(Object sender, NewDataEventArgs args) { newData = true; }
    }
}

