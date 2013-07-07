using System;
using System.IO;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GameRevision.GW2Emu.CodeWriter.CSharp;
using GameRevision.GW2Emu.CodeWriter.Headers;
using GameRevision.GW2Emu.CodeWriter.Packets;
using GameRevision.GW2Emu.CodeWriter.Factory;
using GameRevision.GW2Emu.CodeWriter.Xml;

namespace GameRevision.GW2Emu.CodeWriter
{
    public class CodeWriter
    {
        private const string CSharpExtension = ".cs";

        private string outputDir;
        private Templates templates;

        public CodeWriter(string templatesXml, string outputDir)
        {
            this.LoadTemplates(templatesXml);
            this.outputDir = outputDir;
        }

        private void LoadTemplates(string templatesXml)
        {
            using (TextReader textReader = new StreamReader(templatesXml))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Templates));
                this.templates = (Templates)deserializer.Deserialize(textReader);
            }
        }

        public void WriteCode()
        {
            Parallel.ForEach(this.templates.Protocol, this.WriteCodeFiles);
        }

        private void WriteCodeFiles(CommunicationDirection protocol)
        {
            HeaderGenerator headers = new HeaderGenerator();
            HeaderEnum headerEnum = headers.GenerateMessageHeader(protocol);
            this.WritePacketFiles(headerEnum, protocol);
            this.WriteFactoryFile(headerEnum, protocol);
        }

        private void WritePacketFiles(HeaderEnum headerEnum, CommunicationDirection protocol)
        {
            DateTime date = DateTime.Now;

            Parallel.ForEach(protocol.Packet, delegate(PacketType packet)
            {
                string packetName = headerEnum.NamesByHeader[packet.header];
                string fileName = this.GetMessageFilePath(protocol.type, packetName);

                this.WriteFile(fileName, delegate(CSharpWriter writer)
                {
                    PacketWriter packetWriter = new PacketWriter(writer, protocol, packet, headerEnum, date);
                    packetWriter.WriteMessage();
                });
            });
        }

        private void WriteFactoryFile(HeaderEnum headerEnum, CommunicationDirection protocol)
        {
            if (protocol.type.GetPacketDirection() == PacketDirection.In)
            {
                DateTime date = DateTime.Now;

                string factoryName = protocol.type.GetServerName().ToString().Replace("Server", string.Empty) + "MessageFactory";
                string fileName = this.GetFactoryFilePath(protocol.type, factoryName);

                this.WriteFile(fileName, delegate(CSharpWriter writer)
                {
                    FactoryWriter factoryWriter = new FactoryWriter(writer, protocol, headerEnum, date);
                    factoryWriter.WriteFactory();
                });
            }
        }

        private string GetMessageFilePath(ProtocolSimpleTypes type, string messageName)
        {
            string direction = string.Empty;

            if (type.GetPacketDirection() == PacketDirection.In)
            {
                direction = "CtoS";
            }
            else
            {
                direction = "StoC";
            }

            return Path.Combine(outputDir, type.GetServerName().ToString(), direction, messageName + CSharpExtension);
        }

        private string GetFactoryFilePath(ProtocolSimpleTypes type, string factoryName)
        {
            return Path.Combine(outputDir, type.GetServerName().ToString(), factoryName + CSharpExtension);
        }

        private void WriteFile(string filePath, Action<CSharpWriter> write)
        {
            var file = new FileInfo(filePath);

            Directory.CreateDirectory(file.DirectoryName);

            using (StreamWriter sw = file.CreateText())
            using (var indentWriter = new IndentedTextWriter(sw))
            {
                var csharpWriter = new CSharpWriter(indentWriter);

                Console.WriteLine("Writing file: " + file);
                write(csharpWriter);
            }
        }
    }
}
