using Amicitia.IO.Streams;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System.Collections.Generic;
using System.IO;

// Format names:        Acroarts Resource
// Format references:   Sonicteam::Spanverse::AckResource
// Format designers:    Sonic Team
// Format researchers:  Hyper, Rei-san

namespace Marathon.Formats.Acroarts
{
    /// <summary>
    /// Support for *.mab files; used for Acroarts event data.
    /// </summary>
    public class AckResource : FileBase
    {
        private const string _extension = ".mab"; // "My Acroarts Binary" (speculatory)
        private const string _signature = "MRAB"; // "My Resource Acroarts Binary" (speculatory)

        public const uint Version = 2006020901; // 2006 February 9th, Revision 1

        public AcroartsBinaryArchive Archive { get; set; }

        public override string Extension => _extension;

        public AckResource() { }

        public AckResource(string in_path) : base(in_path) { }

        public AckResource(Stream in_stream) : base(in_stream) { }

        public AckResource(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var mrabReader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness);

            mrabReader.CheckSignature(_signature);

            var binaStart = mrabReader.Read<uint>();
            var binaLength = mrabReader.Read<uint>();

            var binaReader = new BINAReader(in_stream, binaStart);

            Endianness = binaReader.Endianness;

            var abdaStart = binaReader.Read<uint>();
            var abrsStart = binaReader.Read<uint>();

            binaReader.JumpTo(binaReader.Offset + BINAHeader.Size + abdaStart);

            Archive = new AcroartsBinaryArchive(binaReader);

            binaReader.JumpTo(binaReader.Offset + BINAHeader.Size + abrsStart);
        }
    }
}
