using System;
using GameRevision.GW2Emu.CodeWriter.CSharp;

namespace GameRevision.GW2Emu.CodeWriter.Packets
{
    static class Serializer
    {
        public const string PacketMethod = "Serialize";

        public const string Type = "Serializer";
        public const string Name = "serializer";

        private const string Prefix = "Write";

        public static string GetMethodCall(params string[] args)
        {
            const string methodName = Prefix;
            return Utilities.GetMethodCall(Name, methodName, args);
        }

        public static string GetSuffixedMethodCall(string suffix, params string[] args)
        {
            string methodName = Prefix + suffix;
            return Utilities.GetMethodCall(Name, methodName, args);
        }

        public static void WriteMethodCall(CSharpWriter writer, params string[] args)
        {
            const string methodName = Prefix;
            writer.WriteMethodCall(Name, methodName, args);
        }

        public static void WriteSuffixedMethodCall(CSharpWriter writer, string suffix, params string[] args)
        {
            string methodName = Prefix + suffix;
            writer.WriteMethodCall(Name, methodName, args);
        }
    }

    static class Deserializer
    {
        public const string PacketMethod = "Deserialize";

        public const string Type = "Deserializer";
        public const string Name = "deserializer";

        private const string Prefix = "Read";

        public static string GetMethodCall(string suffix, params string[] args)
        {
            string methodName = Prefix + suffix;
            return Utilities.GetMethodCall(Name, methodName, args);
        }
    }
}
