using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler;

namespace Marathon.Formats.Script.Lua
{
    /// <summary>
    /// File base for the *.lub format.
    /// <para>Used in SONIC THE HEDGEHOG for compiled Lua scripts.</para>
    /// </summary>
    public class LuaBinary : FileBase
    {
        public IndentationType IndentationType { get; set; } = IndentationType.Spaces;

        public LuaBinary() { }

        public LuaBinary(string file, bool decompile = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".lua":
                case ".lub":
                {
                    Load(file);

                    if (decompile)
                        Save(file);

                    break;
                }
            }
        }

        private LFunction _main;
        public LFunction Main 
        { 
            get => _main;

            private set
            {
                _main = value;
                DecompileCache = null;
            }
        }

        private string DecompileCache { get; set; }

        public override void Load(Stream stream)
        {
            BinaryReaderEx reader = new(stream);

            BHeader header = new(reader);
            Main = header.Function.Parse(reader, header);
        }

        public string Decompile()
        {
            if (string.IsNullOrEmpty(DecompileCache))
            {
                Decompiler.Decompiler unlub = new(Main);
                unlub.Decompile();

                var provider = new OutputProviderString();
                unlub.Write(new Output(provider, IndentationType));

                DecompileCache = provider.ToString();
            }

            return DecompileCache;
        }

        public override void Save(Stream stream)
        {
            Decompiler.Decompiler unlub = new(Main);
            unlub.Decompile();

            using var provider = new OutputProviderStream(stream);
            unlub.Write(new Output(provider, IndentationType));
        }

        private class OutputProviderString : IOutputProvider
        {
            private StringBuilder _pout = new();

            public void Write(string str)
                => _pout.Append(str);

            public void WriteLine()
                => _pout.AppendLine();

            public override string ToString() => _pout.ToString();
        }

        private class OutputProviderStream : IOutputProvider, IDisposable
        {
            private StreamWriter _out;

            public OutputProviderStream(Stream stream) => _out = new StreamWriter(stream, leaveOpen: true);

            public void Write(string str)
                => _out.Write(str);

            public void WriteLine()
                => _out.WriteLine();

            public void Dispose() 
                => _out.Dispose();
        }
    }
}
