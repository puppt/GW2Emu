using System;
using System.Linq;

namespace GameRevision.GW2Emu.CodeWriter.CSharp
{
    public static class Utilities
    {
        public static string GetMethodCall(string target, string method, params string[] args)
        {
            return target + "." + method + "(" + String.Join(", ", args.Where(item => !String.IsNullOrEmpty(item))) + ")";
        }
    }
}
