using System;
using System.Linq;
using System.CodeDom.Compiler;

namespace GameRevision.GW2Emu.CodeWriter.CSharp
{
    public class CSharpWriter : IDisposable
    {
        private const string Access = "public";

        private IndentedTextWriter writer;

        public CSharpWriter(IndentedTextWriter writer)
        {
            this.writer = writer;
        }

        public void WriteUsing(string name)
        {
            this.writer.WriteLine("using {0};", name);
        }

        public void WriteNamespace(string name)
        {
            this.writer.WriteLine("namespace GameRevision.GW2Emu." + name);
        }

        public void WriteInBlock(Action writeAction)
        {
            this.WriteStartBlock();
            writeAction();
            this.WriteEndBlock();
        }

        private void WriteStartBlock()
        {
            this.writer.WriteLine("{");
            this.writer.Indent++;
        }

        private void WriteEndBlock()
        {
            this.writer.Indent--;
            this.writer.WriteLine("}");
        }

        public void WriteClass(string name)
        {
            this.writer.WriteLine(Access + " class " + name);
        }

        public void WriteClass(string name, string baseClass)
        {
            this.writer.WriteLine(Access + " class " + name + " : " + baseClass);
        }

        public void WriteStruct(string name)
        {
            this.writer.WriteLine(Access + " struct " + name);
        }

        public void WriteField(string type, string name)
        {
            this.writer.WriteLine(Access + " " + type + " " + name + ";");
        }

        public void WriteProperty(string type, string name)
        {
            this.writer.WriteLine(Access + " " + type + " " + name);
        }

        public void WriteOverridingProperty(string type, string name)
        {
            this.writer.WriteLine(Access + " override " + type + " " + name);
        }

        public void WriteAutomaticProperty(string type, string name, string getAccess, string setAccess)
        {
            this.writer.WriteLine(Access + " " + type + " " + name + " { " + getAccess + " get; " + setAccess + " set; }");
        }

        public void WriteGet()
        {
            this.writer.WriteLine("get");
        }

        public void WriteSet()
        {
            this.writer.WriteLine("set");
        }

        public void WriteEnum(string name, string baseType)
        {
            this.writer.WriteLine(Access + " enum " + name + " : " + baseType);
        }

        public void WriteEnumNameValue(string name, string value)
        {
            this.writer.WriteLine(name + " = " + value + ",");
        }

        public void WriteAssignStatement(string left, string right)
        {
            this.writer.WriteLine(left + " = " + right + ";");
        }

        public void WriteMethodCall(string target, string method, params string[] args)
        {
            this.writer.WriteLine(Utilities.GetMethodCall(target, method, args) + ";");
        }

        public void WriteIf(string condition)
        {
            this.writer.WriteLine("if (" + condition + ")");
        }

        public void WriteElseIf(string condition)
        {
            this.writer.WriteLine("else if (" + condition + ")");
        }

        public void WriteElse()
        {
            this.writer.WriteLine("else");
        }

        public void WriteForLoop(string condition)
        {
            this.writer.WriteLine("for (int i = 0; i < " + condition + "; i++)");
        }

        public void WriteSwitch(string condition)
        {
            this.writer.WriteLine("switch (" + condition + ")");
        }

        public void WriteCase(string expression, Action writeAction)
        {
            this.writer.WriteLine("case " + expression + ":");
            this.writer.Indent++;
            writeAction();
            this.writer.Indent--;
        }

        public void WriteDefault(Action writeAction)
        {
            this.writer.WriteLine("default:");
            this.writer.Indent++;
            writeAction();
            this.writer.Indent--;
        }

        public void WriteThrowException(string name)
        {
            this.writer.WriteLine("throw new " + name + "();");
        }

        public void WriteMethod(string method, string argument)
        {
            this.writer.WriteLine(Access + " void " + method + "(" + argument + ")");
        }

        public void WriteMethod(string type, string method, string argument)
        {
            this.writer.WriteLine(Access + " " + type + " " + method + "(" + argument + ")");
        }

        public void WriteOverridingMethod(string method, string argument)
        {
            this.writer.WriteLine(Access + " override void " + method + "(" + argument + ")");
        }

        public void WriteReturn(string nameOrConst)
        {
            this.writer.WriteLine("return " + nameOrConst + ";");
        }

        public void WriteLine()
        {
            this.writer.WriteLine();
        }

        public void WriteConstructor(string name, string argument)
        {
            this.writer.WriteLine(Access + " " + name + "(" + argument + ")");
        }

        public void WriteVariable(string type, string name)
        {
            this.writer.WriteLine(type + " " + name + ";");
        }

        public void WriteVariable(string modifier, string type, string name)
        {
            this.writer.Write(modifier);
            this.WriteVariable(type, name);
        }

        public void WriteComment(string comment)
        {
            this.writer.WriteLine("// " + comment);
        }

        public void WriteMultilineComment(params string[] comments)
        {
            this.writer.WriteLine("/*");

            foreach (string comment in comments)
            {
                this.writer.WriteLine(" * " + comment);
            }

            this.writer.WriteLine(" */");
        }

        public void Dispose()
        {
            this.writer.Dispose();
        }
    }
}
