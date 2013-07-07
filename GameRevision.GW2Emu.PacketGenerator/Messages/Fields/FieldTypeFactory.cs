using System;
using GameRevision.GW2Emu.CodeWriter.CSharp;

namespace GameRevision.GW2Emu.CodeWriter.Messages.Fields
{
    internal static class FieldTypeFactory
    {
        public static FieldType Create(BasicFieldType basicFieldType, CSharpWriter writer)
        {
            switch (basicFieldType.type)
            {
                case PacketSimpleTypes.@byte:
                    return new ByteFieldType(writer);
                case PacketSimpleTypes.@short:
                    return new ShortFieldType(writer);
                case PacketSimpleTypes.@long:
                    return new LongFieldType(writer);
                case PacketSimpleTypes.varint:
                    return new VarintFieldType(writer);
                case PacketSimpleTypes.@float:
                    return new FloatFieldType(writer);
                case PacketSimpleTypes.vec2:
                    return new Vector2FieldType(writer);
                case PacketSimpleTypes.vec3:
                    return new Vector3FieldType(writer);
                case PacketSimpleTypes.vec4:
                    return new Vector4FieldType(writer);
                case PacketSimpleTypes.dw3:
                    return new WorldPositionFieldType(writer);
                case PacketSimpleTypes.uid16:
                    return new UIDFieldType(writer);
                case PacketSimpleTypes.guid18:
                    return new IPEndPointFieldType(writer);
                case PacketSimpleTypes.ascii:
                    return new AsciiFieldType(writer, 0); // TODO: Assign value from templates
                case PacketSimpleTypes.utf16:
                    return new Utf16FieldType(writer, 0); // TODO: Assign value from templates
                case PacketSimpleTypes.optional:
                case PacketSimpleTypes.array_static:
                case PacketSimpleTypes.array_var_small:
                case PacketSimpleTypes.array_var_big:
                case PacketSimpleTypes.buffer_static:
                case PacketSimpleTypes.buffer_var_small:
                case PacketSimpleTypes.buffer_var_big:
                    {
                        var structType = (StructFieldType) basicFieldType;
                        return CreateOuterType(structType, writer);
                    }
                case PacketSimpleTypes.varbyte:
                    var varbyte = (VarByteType) basicFieldType;
                    return new VarbyteFieldType(writer, int.Parse(varbyte.srcBytes));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static OuterFieldType CreateOuterType(StructFieldType structure, CSharpWriter writer)
        {
            InnerFieldType innerType = CreateInnerType(structure, writer);
            switch (structure.type)
            {
                case PacketSimpleTypes.optional:
                    return new OptionalFieldType(writer, innerType);
                case PacketSimpleTypes.array_static:
                case PacketSimpleTypes.buffer_static:
                    {
                        var staticArr = (StaticArrayFieldType) structure;
                        return new StaticArrayType(writer, innerType, staticArr.elements);
                    }
                case PacketSimpleTypes.array_var_small:
                case PacketSimpleTypes.array_var_big:
                case PacketSimpleTypes.buffer_var_small:
                case PacketSimpleTypes.buffer_var_big:
                    {
                        return CreateVarArray(structure, innerType, writer);
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static VarArrayFieldType CreateVarArray(StructFieldType structure, InnerFieldType innerType,
                                                        CSharpWriter writer)
        {
            int maxLength = int.Parse(structure.staticSize) - sizeof (int);
            switch (structure.type)
            {
                case PacketSimpleTypes.buffer_var_big:
                case PacketSimpleTypes.array_var_big:
                    return new VarBigArrayFieldType(writer, innerType, maxLength);
                case PacketSimpleTypes.array_var_small:
                case PacketSimpleTypes.buffer_var_small:
                    return new VarSmallArrayFieldType(writer, innerType, maxLength);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static InnerFieldType CreateInnerType(StructFieldType structure, CSharpWriter writer)
        {
            if (structure.type == PacketSimpleTypes.buffer_static ||
                structure.type == PacketSimpleTypes.buffer_var_small ||
                structure.type == PacketSimpleTypes.buffer_var_big)
                return new ByteFieldType(writer);

            if (structure.Field.Length == 1)
            {
                return (InnerFieldType) Create(structure.Field[0], writer);
            }

            return new InnerStructFieldType(writer);
        }
    }
}