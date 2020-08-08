using System.IO;
using System.Text;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler;

namespace Marathon.IO.Formats.Scripts.Lua
{
    public class unluac
    {
        public static LFunction file_to_function(string fn)
        {
            FileStream file = File.OpenRead(fn);
            ExtendedBinaryReader buffer = new ExtendedBinaryReader(file) { IsBigEndian = false };

            BHeader header = new BHeader(buffer);
            return header.function.parse(buffer, header);
        }

        public static void decompile(string @in, string @out)
        {
            LFunction lmain = file_to_function(@in);

            Decompiler.Decompiler d = new Decompiler.Decompiler(lmain);
            d.decompile();

            StringBuilder pout = new StringBuilder();

            d.print(new Output(new OutputProviderAnonymousInnerClass(pout)));
        }

        private class OutputProviderAnonymousInnerClass : IOutputProvider
        {
            private StringBuilder pout;

            public OutputProviderAnonymousInnerClass(StringBuilder pout) => this.pout = pout;

            public void print(string s) => pout.Append(s);

            public void println() => pout.AppendLine();
        }
    }
}
