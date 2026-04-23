using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Formats.Script.Lua.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
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

        private string _decompiled;

        public IndentationType IndentationType { get; set; } = IndentationType.Spaces;

        public LFunction Main 
        {
            get;

            private set
            {
                field = value;
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

        public void Export(string in_path = "", SymbolResolverOptions in_symbolResolverOptions = null, string in_symbolsPath = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path);

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            File.WriteAllText(in_path, Decompile(in_symbolResolverOptions, in_symbolsPath));
        }

        public void Export(SymbolResolverOptions in_symbolResolverOptions, string in_symbolsPath = "", bool in_overwrite = true)
        {
            Export(string.Empty, in_symbolResolverOptions, in_symbolsPath, in_overwrite);
        }

        public void LoadSymbols(string in_path)
        {
            SymbolResolver.LoadSymbols(in_path);
        }

        public void LoadSymbols(List<Symbol> in_symbols)
        {
            SymbolResolver.LoadSymbols(in_symbols);
        }

        public string Decompile(SymbolResolverOptions in_symbolResolverOptions = null, string in_symbolsPath = "")
        {
            if (!string.IsNullOrEmpty(_decompiled))
                return _decompiled;

            if (!string.IsNullOrEmpty(in_symbolsPath))
                LoadSymbols(in_symbolsPath);

            SymbolResolver.SetOptions(in_symbolResolverOptions);

            var decompiler = new Decompiler.Decompiler(Main);
            decompiler.Decompile();

            var outputProvider = new OutputProviderString();

            decompiler.Write(new Output(outputProvider, IndentationType));

            return _decompiled = outputProvider.ToString();
        }
    }
}
