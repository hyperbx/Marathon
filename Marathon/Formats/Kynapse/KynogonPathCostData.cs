using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System;
using System.IO;

// Format names:        Kynogon Path Cost Data
// Format references:   Kaim::CPathCostData
// Format designers:    Kynogon
// Format researchers:  Hyper
//
// Format research references:
// - Sacred 2: Fallen Angel for Kynapse symbols.

namespace Marathon.Formats.Kynapse
{
    /// <summary>
    /// Support for *.cdl files; used for Kynapse path cost heuristics.
    /// </summary>
    [FileType("Kynogon Path Cost Data", "Kynapse", _extension)]
    public class KynogonPathCostData : FileBase
    {
        private const string _extension = ".cdl"; // "path Cost Data List" (made by Marathon, may not be correct)
        private const string _signature = "Kynogon PathCost Data";
        private const int _version = 1;

        public float Ratio { get; set; }

        public byte[] TabPathLength { get; set; }

        public override string Extension => _extension;

        public KynogonPathCostData() { }

        public KynogonPathCostData(string in_path) : base(in_path) { }

        public KynogonPathCostData(Stream in_stream) : base(in_stream) { }

        public KynogonPathCostData(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            var version = reader.Read<int>();

            if (version != _version)
                throw new InvalidSignatureException(_version, version);

            Ratio = reader.Read<float>();
            TabPathLength = reader.ReadArray<byte>((int)reader.Length - (int)reader.Position);
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(_version);
            writer.Write(Ratio);
            writer.WriteArray(TabPathLength);
        }

        public int GetPointCount()
        {
            if (TabPathLength.Length <= 0)
                return 0;

            var result = Math.Sqrt(TabPathLength.Length);

            if ((result % 1) != 0)
                throw new InvalidDataException("Invalid path cost data.");

            return (int)result;
        }
    }
}
