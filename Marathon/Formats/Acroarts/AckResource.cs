using Amicitia.IO.Streams;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System.IO;

// Format names:        Acroarts Resource
// Format references:   Sonicteam::Spanverse::AckResource
// Format designers:    Sonic Team
// Format researchers:  Hyper, Rei-san

namespace Marathon.Formats.Acroarts
{
    /// <summary>
    /// Support for *.mab files; used for Acroarts data.
    /// </summary>
    public class AckResource : FileBase
    {
        private const string _extension = ".mab"; // "Merged Acroarts Binary" (speculatory)
        private const string _signature = "MRAB"; // "MeRged Acroarts Binary" (speculatory)

        private bool _binaHeaderHasSignature = true;

        public const uint Version = 2006020901;   // 2006 February 9th, Revision 1

        public DataChunk Data { get; set; }

        public ResourceChunk Resources { get; set; }

        public override string Extension => _extension;

        public AckResource() { }

        public AckResource(string in_path) : base(in_path) { }

        public AckResource(Stream in_stream) : base(in_stream) { }

        public AckResource(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var mrabReader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness);

            mrabReader.CheckSignature(_signature);

            var binaOffset = mrabReader.Read<uint>();
            var binaLength = mrabReader.Read<uint>();

            mrabReader.JumpAhead(4); // Reserved.

            var binaReader = new BINAReader(in_stream, binaOffset);

            Endianness = binaReader.Endianness;

            _binaHeaderHasSignature = binaReader.Header.HasSignature;

            var abdaOffset = binaReader.Read<uint>();
            var abrsOffset = binaReader.Read<uint>();

            if (abdaOffset != 0)
            {
                binaReader.JumpTo(binaReader.CalculateOffset(abdaOffset));
                Data = new DataChunk(binaReader);
            }

            if (abrsOffset != 0)
            {
                binaReader.JumpTo(binaReader.CalculateOffset(abrsOffset));
                Resources = new ResourceChunk(binaReader);
            }
        }

        public override void Write(Stream in_stream)
        {
            var mrabWriter = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness);

            mrabWriter.WriteSignature(_signature);

            var binaOffset = mrabWriter.Reserve<uint>(true);
            var binaLength = mrabWriter.Reserve<uint>(true);

            mrabWriter.WriteZero<int>(); // Reserved.
            mrabWriter.WriteReserved(binaOffset, (uint)mrabWriter.Position);

            var binaWriter = new BINAWriter(in_stream, mrabWriter.Position, mrabWriter.Endianness);
            {
                binaWriter.Header.HasSignature = _binaHeaderHasSignature;
            }

            var abdaOffset = binaWriter.Reserve<uint>(true);
            var abrsOffset = binaWriter.Reserve<uint>(true);

            binaWriter.WriteZero<byte>(0x18); // Reserved.

            var abdaWriter = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, binaWriter.Endianness);

            abdaWriter.JumpTo(binaWriter.Position);

            if (Data != null)
            {
                binaWriter.WriteReserved(abdaOffset, (uint)(binaWriter.Position - abdaOffset));
                Data.Write(abdaWriter);
            }

            var abrsWriter = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, binaWriter.Endianness);

            abrsWriter.JumpTo(binaWriter.Position);

            if (Resources != null)
            {
                binaWriter.WriteReserved(abrsOffset, (uint)(binaWriter.Position - abrsOffset) + sizeof(uint));
                Resources.Write(abrsWriter);
            }

            // BINA is exclusively used for these.
            binaWriter.AddOffset(abdaOffset);
            binaWriter.AddOffset(abrsOffset);

            binaWriter.FinishWrite();

            mrabWriter.WriteReserved(binaLength, binaWriter.Header.Length);
        }
    }
}
