using System;

namespace GameRevision.GW2Emu.CodeWriter.Xml
{
    public enum ServerName
    {
        LoginServer,
        GameServer
    }

    public enum PacketDirection
    {
        In,
        Out
    }

    public static class TemplatesExtensions
    {
        public static ServerName GetServerName(this ProtocolSimpleTypes protocol)
        {
            switch (protocol)
            {
                case ProtocolSimpleTypes.CtoLS:
                case ProtocolSimpleTypes.LStoC:
                    return ServerName.LoginServer;
                case ProtocolSimpleTypes.CtoGS:
                case ProtocolSimpleTypes.GStoC:
                    return ServerName.GameServer;
                default:
                    throw new ArgumentOutOfRangeException("protocol");
            }
        }

        public static PacketDirection GetPacketDirection(this ProtocolSimpleTypes protocol)
        {
            switch (protocol)
            {
                case ProtocolSimpleTypes.CtoLS:
                    return PacketDirection.In;
                case ProtocolSimpleTypes.CtoGS:
                    return PacketDirection.In;
                case ProtocolSimpleTypes.LStoC:
                    return PacketDirection.Out;
                case ProtocolSimpleTypes.GStoC:
                    return PacketDirection.Out;
                default:
                    throw new ArgumentOutOfRangeException("protocol");
            }
        }

        public static bool HasName(this UserNamedType type)
        {
            return type.Info != null && type.Info.Name != null && type.Info.Name.Length > 0;
        }

        public static string GetName(this UserNamedType type)
        {
            return type.Info.Name;
        }
    }
}