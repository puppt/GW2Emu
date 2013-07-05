using System;
using System.Net.Sockets;
using GameRevision.GW2Emu.Core;
using GameRevision.GW2Emu.Core.EventDesign;
using GameRevision.GW2Emu.Network;

namespace GameRevision.GW2Emu.LoginServer
{
    public class LoginSession : ISession
    {
        public INetworkSession NetworkSession { get; private set; }

        public LoginSession(INetworkSession networkSession)
        {
            this.NetworkSession = networkSession;
            this.NetworkSession.DataReceived += OnDataReceived;
            this.NetworkSession.Run();
        }

        private void OnDataReceived(INetworkSession networkSession, DataReceivedEventArgs e)
        {
            string text = System.Text.Encoding.ASCII.GetString(e.Buffer, 0, e.BytesRead);
            Console.WriteLine();
            Console.WriteLine(text);
        }
    }
}
