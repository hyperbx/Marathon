using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System.Collections.Generic;
using System.IO;

// Format names:        Kynogon Find Nearest Data
// Format references:   Kaim::CFindNearestData
// Format designers:    Kynogon
// Format researchers:  Hyper
//
// Format research references:
// - Fable II for Kynapse file extensions.
// - Sacred 2: Fallen Angel for Kynapse symbols.

namespace Marathon.Formats.Kynapse
{
    /// <summary>
    /// Support for *.fdl files; used for Kynapse nearest neighbour traversal data.
    /// </summary>
    [FileType("Kynogon Find Nearest Data", "Kynapse", _extension)]
    public class KynogonFindNearestData : FileBase
    {
        private const string _extension = ".fdl"; // "Find nearest Data List" (speculatory; from Fable II, may not be correct)
        private const string _signature = "Kynogon FindNearest Data";
        private const int _version = 3;

        public List<int> Sorts { get; set; } = [];

        public List<int> Ranks { get; set; } = [];

        public override string Extension => _extension;

        public KynogonFindNearestData() { }

        public KynogonFindNearestData(string in_path) : base(in_path) { }

        public KynogonFindNearestData(Stream in_stream) : base(in_stream) { }

        public KynogonFindNearestData(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            var version = reader.Read<int>();

            if (version != _version)
                throw new InvalidSignatureException(_version, version);

            reader.JumpAhead(sizeof(long)); // Padding.

            var pointCount = reader.Read<int>();

            if (pointCount <= 0)
                return;

            reader.ReadCollection((pointCount * 12) / sizeof(int), Sorts);
            reader.ReadCollection((pointCount * 12) / sizeof(int), Ranks);
        }

        public override void Write(Stream in_stream)
        {
            if (Sorts.Count != Ranks.Count)
                throw new InvalidDataException("Mismatching sorts and ranks count.");

            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(_version);
            writer.Write(0xAAAAAAAAAAAAAAAA);

            var pointCount = Sorts.Count / 3;

            writer.Write(pointCount);

            if (pointCount <= 0)
                return;

            writer.WriteCollection(Sorts);
            writer.WriteCollection(Ranks);

            for (int i = 0; i < pointCount; i++)
                writer.WriteZero<int>();
        }
    }
}
