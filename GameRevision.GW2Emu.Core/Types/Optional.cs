using System;

namespace GameRevision.GW2Emu.Core.Types
{
    public class Optional<T> where T : struct
    {
        private bool isPresent;
        private T value;

        public Optional(T value)
        {
            if (this.IsNullable(value) && value.Equals(default(T)))
            {
                throw new ArgumentNullException("value");
            }

            this.isPresent = true;
            this.value = value;
        }

        private bool IsNullable(T t)
        {
            return false;
        }

        private bool IsNullable(T? t)
        {
            return true;
        }

        public bool IsPresent
        {
            get
            {
                return this.isPresent;
            }
        }

        public T Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
