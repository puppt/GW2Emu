using System;
using GameRevision.GW2Emu.Core;
using GameRevision.GW2Emu.Core.Types;
using CtoS = GameRevision.GW2Emu.Messages.LoginServer.CtoS;
using StoC = GameRevision.GW2Emu.Messages.LoginServer.StoC;

namespace GameRevision.GW2Emu.LoginServer.Handlers
{
    public class Login : IRegisterable
    {
        public void Register(IEventAggregator aggregator)
        {
            aggregator.Register<CtoS.P03_ComputerInfoMessage>(this.OnComputerInfo);
            aggregator.Register<CtoS.P10_ClientSessionInfoMessage>(this.OnClientSessionInfo);
        }

        private void OnComputerInfo(CtoS.P03_ComputerInfoMessage message)
        {
            ISession session = message.Session;
            session.SendMessage(new StoC.P02_ComputerInfoReplyMessage());
            session.SendMessage(new StoC.P01_UnknownMessage());
        }

        private void OnClientSessionInfo(CtoS.P10_ClientSessionInfoMessage message)
        {
            ISession session = message.Session;
        }
    }
}
