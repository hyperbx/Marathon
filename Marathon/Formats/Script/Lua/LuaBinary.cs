using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Types.FileSystem;
using System;
using System.IO;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG
//
// Format research references:
// - https://sourceforge.net/projects/unluac

namespace Marathon.Formats.Script.Lua
{
    /// <summary>
    /// Support for *.lub files; used for compiled Lua scripts.
    /// </summary>
    [FileType("Lua Binary", "Script", _extension)]
    public class LuaBinary : FileBase
    {
        private const string _extension = ".lub"; // "LUa Binary"

        private LFunction _main;
        private string _decompiled;

        public IndentationType IndentationType { get; set; } = IndentationType.Spaces;

        public LFunction Main 
        {
            get => _main;

            private set
            {
                _main = value;
                _decompiled = null;
            }
        }

        public override string Extension => _extension;

        public LuaBinary() { }

        public LuaBinary(string in_path) : base(in_path) { }

        public LuaBinary(Stream in_stream) : base(in_stream) { }

        public LuaBinary(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);
            var header = new BHeader(reader);

            Main = header.Function.Parse(reader, header);
        }

        public override void Write(Stream in_stream)
        {
            throw new NotImplementedException();
        }

        public override void Import(string in_path)
        {
            throw new NotImplementedException();
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path);

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            File.WriteAllText(in_path, Decompile());
        }

        public string Decompile()
        {
            if (!string.IsNullOrEmpty(_decompiled))
                return _decompiled;

            var decompiler = new Decompiler.Decompiler(Main);
            decompiler.Decompile();

            var ops = new OutputProviderString();

            decompiler.Write(new Output(ops, IndentationType));

            return _decompiled = ops.ToString();
        }
    }
}
