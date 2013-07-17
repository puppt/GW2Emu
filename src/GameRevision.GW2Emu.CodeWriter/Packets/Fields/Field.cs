using System;

namespace GameRevision.GW2Emu.CodeWriter.Packets.Fields
{
    internal class Field
    {
        public readonly string Name;

        public readonly FieldType Type;

        public Field(string name, FieldType type)
        {
            Name = name;
            Type = type;
        }

        public void Write()
        {
            Type.WriteField(Name);
        }

        public void WriteSerializer()
        {
            Type.WriteSerializer(Name);
        }

        public void WriteDeserializer()
        {
            Type.WriteDeserializer(Name);
        }
    }
}