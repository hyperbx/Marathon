using System;
using System.IO;
using System.Text;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class OutputProviderStream(Stream in_stream) : IOutputProvider, IDisposable
    {
        private readonly StreamWriter _sw = new(in_stream, Encoding.UTF8, 1024, leaveOpen: true);

        public void Write(string in_str)
        {
            _sw.Write(in_str);
        }

        public void WriteLine()
        {
            _sw.WriteLine();
        }

        public void Dispose()
        {
            _sw.Dispose();
        }
    }
}
