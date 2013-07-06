using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace GameRevision.GW2Emu.Core
{
    public class ConcurrentSessionCollection
    {
        private readonly object listLock = new object();
        private IList<ISession> list;

        public ConcurrentSessionCollection()
        {
            this.list = new List<ISession>();
        }

        public ISession this[int index]
        {
            get
            {
                lock (this.listLock)
                {
                    return this.list[index];
                }
            }
            set
            {
                lock (this.listLock)
                {
                    this.list[index] = value;
                }
            }
        }

        public void Add(ISession session)
        {
            lock (this.listLock)
            {
                this.list.Add(session);
            }
        }

        public void Remove(ISession session)
        {
            lock (this.listLock)
            {
                this.list.Remove(session);
            }
        }

        public void Stop(ISession session)
        {
            lock (this.listLock)
            {
                session.Stop();
                this.list.Remove(session);
            }
        }

        public void StopAll()
        {
            lock (this.listLock)
            {
                foreach (ISession session in this.list)
                {
                    session.Stop();
                    this.list.Remove(session);
                }
            }
        }
    }
}
