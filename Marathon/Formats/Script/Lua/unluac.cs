using System.IO;
using System.Text;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.IO;

namespace Marathon.Formats.Script.Lua
{
    public class unluac
    {
        public static LFunction ParseFileToFunction(string file)
        {
            BinaryReaderEx reader = new(File.OpenRead(file));

            BHeader header = new(reader);
            return header.Function.Parse(reader, header);
        }

        public static void Decompile(string @in, string @out)
        {
            LFunction lmain = ParseFileToFunction(@in);

            Decompiler.Decompiler d = new(lmain);
            d.Decompile();

            StringBuilder pout = new();
            d.Write(new Output(new OutputProviderAnonymousInnerClass(pout)));

            File.WriteAllText(@out, pout.ToString());
        }

        private class OutputProviderAnonymousInnerClass : IOutputProvider
        {
            private StringBuilder _pout;

            public OutputProviderAnonymousInnerClass(StringBuilder pout) => _pout = pout;

            public void Write(string str)
                => _pout.Append(str);

            public void WriteLine()
                => _pout.AppendLine();
        }
    }
}
