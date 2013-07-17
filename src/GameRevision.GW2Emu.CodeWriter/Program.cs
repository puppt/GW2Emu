using System;

namespace GameRevision.GW2Emu.CodeWriter
{
    public class Program
    {
        private const string OutputDirectory = "Messages";

        public static void Main()
        {
            CodeWriter codeWriter = new CodeWriter(@"Xml\Templates.xml", OutputDirectory);
            codeWriter.WriteCode();

            Console.WriteLine("Press any key to close the program. . .");
            Console.ReadKey();
        }
    }
}
