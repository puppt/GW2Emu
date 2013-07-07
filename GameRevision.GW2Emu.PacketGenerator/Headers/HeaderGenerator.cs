using System;
using GameRevision.GW2Emu.CodeWriter.Xml;

namespace GameRevision.GW2Emu.CodeWriter.Headers
{
    public class HeaderGenerator
    {
        private HeaderEnum headerEnum;

        public HeaderEnum GenerateMessageHeader(CommunicationDirection protocol)
        {
            this.CreateHeaderEnum(protocol);
            this.GenerateHeaderNamePairs(protocol);
            return this.headerEnum;
        }

        private void CreateHeaderEnum(CommunicationDirection protocol)
        {
            ProtocolSimpleTypes type = protocol.type;

            string enumName = type.GetServerName().ToString().Replace("Server", "")
                              + "MessageHeader" + type.GetPacketDirection();

            string @namespace = type.GetServerName() + "." + type.GetPacketDirection();

            this.headerEnum = new HeaderEnum(enumName, @namespace);
        }

        private void GenerateHeaderNamePairs(CommunicationDirection protocol)
        {
            bool large = (protocol.type == ProtocolSimpleTypes.CtoGS || protocol.type == ProtocolSimpleTypes.GStoC);

            foreach (PacketType packet in protocol.Packet)
            {
                GenerateHeaderNamePair(packet, large);
            }
        }

        private void GenerateHeaderNamePair(PacketType packet, bool large)
        {
            int header = int.Parse(packet.header);
            string zeroes = string.Empty;

            if (large)
            {
                if (header < 10)
                {
                    zeroes = "00";
                }
                else if (header < 100)
                {
                    zeroes = "0";
                }
            }
            else
            {
                if (header < 10)
                {
                    zeroes = "0";
                }
            }

            string name = packet.HasName() ? packet.GetName() : "P" + zeroes + packet.header + "_UnknownMessage";
            headerEnum.NamesByHeader.Add(packet.header, name);
        }
    }
}
