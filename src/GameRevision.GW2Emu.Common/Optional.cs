using System;

namespace GameRevision.GW2Emu.Common
{
    public class Optional<T> where T : struct
    {

        public bool IsPresent { get; private set; }
        public T Value  { get; private set; }


        public Optional()
        {
            this.IsPresent = false;
        }


        public Optional(T value)
        {
            this.IsPresent = true;
            this.Value = value;
        }
    }
}

