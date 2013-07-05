using System;

namespace GameRevision.GW2Emu.Core.EventDesign
{
    public class SessionCreatedEventArgs : EventArgs
    {
        public ISession Session { get; private set; }
        public DateTime CreationTime { get; private set; }

        public SessionCreatedEventArgs(ISession session, DateTime creationTime)
        {
            this.Session = session;
            this.CreationTime = creationTime;
        }
    }
}
