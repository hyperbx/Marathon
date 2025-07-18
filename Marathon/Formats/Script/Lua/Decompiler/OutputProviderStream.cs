using System;
using System.IO;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class OutputProviderStream(Stream in_stream) : IOutputProvider, IDisposable
    {
        private readonly StreamWriter _sw = new(in_stream, leaveOpen: true);

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
