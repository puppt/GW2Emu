using System;
using System.Collections.Generic;

namespace GameRevision.GW2Emu.CodeWriter.Headers
{
    public class HeaderEnum
    {
        public readonly string Name;
        public readonly string Namespace;

        public readonly Dictionary<string, string> NamesByHeader = new Dictionary<string, string>();

        public HeaderEnum(string name, string @namespace)
        {
            Name = name;
            Namespace = @namespace;
        }
    }
}
