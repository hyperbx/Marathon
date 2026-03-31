using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System.Collections.Generic;
using System.IO;

// Format names:        Kynogon Astar Data
// Format references:   Kaim::CAstarData
// Format designers:    Kynogon
// Format researchers:  Hyper
//
// Format research references:
// - Sacred 2: Fallen Angel for Kynapse symbols.

namespace Marathon.Formats.Kynapse
{
    /// <summary>
    /// Support for *.adl files; used for Kynapse A* data.
    /// </summary>
    [FileType("Kynogon A* Data", "Kynapse", _extension)]
    public class KynogonAstarData : FileBase
    {
        private const string _extension = ".adl"; // "Astar Data List" (made by Marathon, may not be correct)
        private const string _signature = "Kynogon Astar Data";
        private const int _version = 1;

        public List<int> Data { get; set; } = [];

        public override string Extension => _extension;

        public KynogonAstarData() { }

        public KynogonAstarData(string in_path) : base(in_path) { }

        public KynogonAstarData(Stream in_stream) : base(in_stream) { }

        public KynogonAstarData(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            var version = reader.Read<int>();

            if (version != _version)
                throw new InvalidSignatureException(_version, version);

            while (reader.Position < reader.Length)
                Data.Add(reader.Read<int>());
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(_version);
            writer.WriteCollection(Data);
        }
    }
}
