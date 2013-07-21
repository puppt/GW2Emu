using System;
using GameRevision.GW2Emu.CodeWriter.CSharp;

namespace GameRevision.GW2Emu.CodeWriter.Packets.Fields
{
    //TODO: Fix write postfixes
    internal abstract class FieldType
    {
        protected CSharpWriter Writer;

        protected FieldType(CSharpWriter Writer)
        {
            this.Writer = Writer;
        }

        public abstract string CSharpType { get; }

        public abstract void WriteSerializer(string fieldName);
        public abstract void WriteDeserializer(string fieldName);

        public void WriteField(string fieldName)
        {
            this.Writer.WriteField(CSharpType, fieldName);
        }
    }

    internal abstract class InnerFieldType : FieldType
    {
        protected InnerFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public abstract string DeserializerCall { get; }

        public override void WriteDeserializer(string fieldName)
        {
            this.Writer.WriteAssignStatement(fieldName, DeserializerCall);
        }
    }

    internal abstract class SimpleFieldType : InnerFieldType
    {
        protected SimpleFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        protected virtual string SerializerSuffix
        {
            get { return string.Empty; }
        }

        protected abstract string DeserializerSuffix { get; }

        protected virtual string Argument
        {
            get { return string.Empty; }
        }

        public override string DeserializerCall
        {
            get { return Deserializer.GetMethodCall(DeserializerSuffix, Argument); }
        }

        public override void WriteSerializer(string fieldName)
        {
            Serializer.WriteSuffixedMethodCall(this.Writer, SerializerSuffix, fieldName, Argument);
        }
    }

    internal class ByteFieldType : SimpleFieldType
    {
        public ByteFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "byte"; }
        }

        protected override string DeserializerSuffix
        {
            get { return "Byte"; }
        }
    }

    internal class ShortFieldType : SimpleFieldType
    {
        public ShortFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "short"; }
        }

        protected override string DeserializerSuffix
        {
            get { return "Int16"; }
        }
    }

    internal class LongFieldType : SimpleFieldType
    {
        public LongFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "long"; }
        }

        protected override string DeserializerSuffix
        {
            get { return "Int64"; }
        }
    }

    internal class VarintFieldType : SimpleFieldType
    {
        public VarintFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "int"; }
        }

        protected override string DeserializerSuffix
        {
            get { return "Varint"; }
        }

        protected override string SerializerSuffix
        {
            get { return DeserializerSuffix; }
        }
    }

    internal class FloatFieldType : SimpleFieldType
    {
        public FloatFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "float"; }
        }

        protected override string DeserializerSuffix
        {
            get { return typeof (float).Name; }
        }
    }

    internal class Vector2FieldType : SimpleFieldType
    {
        public Vector2FieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "Vector2"; }
        }

        protected override string DeserializerSuffix
        {
            get { return CSharpType; }
        }

        protected override string SerializerSuffix
        {
            get { return CSharpType; }
        }
    }

    internal class Vector3FieldType : SimpleFieldType
    {
        public Vector3FieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "Vector3"; }
        }

        protected override string DeserializerSuffix
        {
            get { return CSharpType; }
        }

        protected override string SerializerSuffix
        {
            get { return CSharpType; }
        }
    }

    internal class Vector4FieldType : SimpleFieldType
    {
        public Vector4FieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "Vector4"; }
        }

        protected override string DeserializerSuffix
        {
            get { return CSharpType; }
        }

        protected override string SerializerSuffix
        {
            get { return CSharpType; }
        }
    }

    internal class WorldPositionFieldType : SimpleFieldType
    {
        public WorldPositionFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "WorldPosition"; }
        }

        protected override string DeserializerSuffix
        {
            get { return CSharpType; }
        }

        protected override string SerializerSuffix
        {
            get { return CSharpType; }
        }
    }

    internal class UIDFieldType : SimpleFieldType
    {
        public UIDFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "UID"; }
        }

        protected override string DeserializerSuffix
        {
            get { return CSharpType; }
        }

        protected override string SerializerSuffix
        {
            get { return CSharpType; }
        }
    }

    internal class IPEndPointFieldType : SimpleFieldType
    {
        public IPEndPointFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override sealed string CSharpType
        {
            get { return "IPEndPoint"; }
        }

        protected override string DeserializerSuffix
        {
            get { return CSharpType; }
        }

        protected override string SerializerSuffix
        {
            get { return CSharpType; }
        }
    }

    internal abstract class StringFieldType : SimpleFieldType
    {
        private readonly int maxLength;

        protected StringFieldType(CSharpWriter Writer, int maxLength) : base(Writer)
        {
            this.maxLength = maxLength;
        }

        public override sealed string CSharpType
        {
            get { return "string"; }
        }

        protected override string Argument
        {
            get { return maxLength == 0 ? string.Empty : maxLength.ToString(); }
        }

        protected override string SerializerSuffix
        {
            get { return DeserializerSuffix; }
        }
    }

    internal class AsciiFieldType : StringFieldType
    {
        public AsciiFieldType(CSharpWriter Writer, int maxLength) : base(Writer, maxLength)
        {
        }

        protected override string DeserializerSuffix
        {
            get { return "AsciiString"; }
        }
    }

    internal class Utf16FieldType : StringFieldType
    {
        public Utf16FieldType(CSharpWriter Writer, int maxLength)
            : base(Writer, maxLength)
        {
        }

        protected override string DeserializerSuffix
        {
            get { return "Utf16String"; }
        }
    }

    internal class VarbyteFieldType : LongFieldType
    {
        private readonly int sourceBytes;

        public VarbyteFieldType(CSharpWriter Writer, int sourceBytes) : base(Writer)
        {
            this.sourceBytes = sourceBytes;
        }

        protected override string DeserializerSuffix
        {
            get { return "Varbyte"; }
        }

        protected override string SerializerSuffix
        {
            get { return DeserializerSuffix; }
        }

        protected override string Argument
        {
            get { return sourceBytes.ToString(); }
        }
    }

    internal abstract class OuterFieldType : FieldType
    {
        public InnerFieldType InnerType;

        protected OuterFieldType(CSharpWriter Writer, InnerFieldType innerType) : base(Writer)
        {
            InnerType = innerType;
        }
    }

    internal class OptionalFieldType : OuterFieldType
    {
        public OptionalFieldType(CSharpWriter Writer, InnerFieldType innerType) : base(Writer, innerType)
        {
        }

        public override string CSharpType
        {
            get { return "Optional<" + InnerType.CSharpType + ">"; }
        }

        public override void WriteSerializer(string fieldName)
        {
            Serializer.WriteMethodCall(this.Writer, fieldName + ".IsPresent ? (byte) 1 : (byte) 0");

            this.Writer.WriteIf(fieldName + ".IsPresent");
            this.Writer.WriteInBlock(() => InnerType.WriteSerializer(fieldName + ".Value"));
        }

        public override void WriteDeserializer(string fieldName)
        {
            this.Writer.WriteIf(Deserializer.GetMethodCall("Boolean"));
            this.Writer.WriteInBlock(delegate
            {
                if (InnerType.GetType() == typeof(InnerStructFieldType))
                {
                    this.Writer.WriteAssignStatement(InnerType.CSharpType + " " + InnerType.CSharpType.ToLower(), InnerType.DeserializerCall);
                    this.Writer.WriteMethodCall(InnerType.CSharpType.ToLower(), Deserializer.PacketMethod, Deserializer.Name);
                    this.Writer.WriteAssignStatement(fieldName, "new " + CSharpType + "(" + InnerType.CSharpType.ToLower() + ")");
                }
                else
                {
                    this.Writer.WriteAssignStatement(fieldName, "new " + CSharpType + "(" + InnerType.DeserializerCall + ")");
                }
            });
                                
            this.Writer.WriteElse();
            this.Writer.WriteInBlock(() => this.Writer.WriteAssignStatement(fieldName, "null"));
        }
    }

    internal abstract class ArrayFieldType : OuterFieldType
    {
        protected ArrayFieldType(CSharpWriter Writer, InnerFieldType innerType) : base(Writer, innerType)
        {
        }

        public override sealed string CSharpType
        {
            get { return InnerType.CSharpType + "[]"; }
        }

        public override void WriteSerializer(string fieldName)
        {
            this.Writer.WriteForLoop(fieldName + ".Length");
            this.Writer.WriteInBlock(() => InnerType.WriteSerializer(fieldName + "[i]"));
        }

        public override void WriteDeserializer(string fieldName)
        {
            this.Writer.WriteAssignStatement(fieldName, "new " + InnerType.CSharpType + "[" + GetLength(fieldName) + "]");
            this.Writer.WriteForLoop(fieldName + ".Length");
            this.Writer.WriteInBlock(() => InnerType.WriteDeserializer(fieldName + "[i]"));
        }

        protected abstract string GetLength(string fieldName);
    }

    internal class StaticArrayType : ArrayFieldType
    {
        private readonly uint length;

        public StaticArrayType(CSharpWriter Writer, InnerFieldType innerType, uint length) : base(Writer, innerType)
        {
            this.length = length;
        }

        protected override string GetLength(string fieldName)
        {
            return length.ToString();
        }
    }

    internal abstract class VarArrayFieldType : ArrayFieldType
    {
        private readonly int maxLength;

        protected VarArrayFieldType(CSharpWriter Writer, InnerFieldType innerType, int maxLength)
            : base(Writer, innerType)
        {
            this.maxLength = maxLength;
        }

        protected abstract string DeserializerSuffix { get; }
        protected abstract string LengthCSharpType { get; }

        protected override sealed string GetLength(string fieldName)
        {
            return fieldName.ToLowerInvariant() + "Length";
        }

        public override void WriteDeserializer(string fieldName)
        {
            string lengthName = GetLength(fieldName);
            this.Writer.WriteAssignStatement(LengthCSharpType + " " + lengthName,
                                        Deserializer.GetMethodCall(DeserializerSuffix));
            this.Writer.WriteIf(lengthName + " > " + maxLength);
            this.Writer.WriteInBlock(() => this.Writer.WriteThrowException("InvalidDataException"));

            base.WriteDeserializer(fieldName);
        }

        public override void WriteSerializer(string fieldName)
        {
            string lengthArgument = "(" + LengthCSharpType + ")" + fieldName + ".Length";
            Serializer.WriteMethodCall(this.Writer, lengthArgument);

            base.WriteSerializer(fieldName);
        }
    }

    internal class VarSmallArrayFieldType : VarArrayFieldType
    {
        public VarSmallArrayFieldType(CSharpWriter Writer, InnerFieldType innerType, int maxLength)
            : base(Writer, innerType, Math.Min(byte.MaxValue, maxLength))
        {
        }

        protected override string DeserializerSuffix
        {
            get { return "Byte"; }
        }

        protected override string LengthCSharpType
        {
            get { return "byte"; }
        }
    }

    internal class VarBigArrayFieldType : VarArrayFieldType
    {
        public VarBigArrayFieldType(CSharpWriter Writer, InnerFieldType innerType, int maxLength)
            : base(Writer, innerType, Math.Min(ushort.MaxValue, maxLength))
        {
        }

        protected override string DeserializerSuffix
        {
            get { return "UInt16"; }
        }

        protected override string LengthCSharpType
        {
            get { return "ushort"; }
        }
    }

    internal class InnerStructFieldType : InnerFieldType
    {
        public string Name;
        public StructFieldType Type;

        public InnerStructFieldType(CSharpWriter Writer) : base(Writer)
        {
        }

        public override string CSharpType
        {
            get { return Name; }
        }

        public override string DeserializerCall
        {
            get { return "new " + Name + "()"; }
        }

        public override void WriteDeserializer(string fieldName)
        {

            base.WriteDeserializer(fieldName);
        }

        public override void WriteSerializer(string fieldName)
        {
            this.Writer.WriteMethodCall(fieldName, Serializer.PacketMethod, Serializer.Name);
        }
    }
}