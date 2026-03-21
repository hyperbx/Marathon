using Amicitia.IO.Binary;
using Marathon.Helpers;
using Marathon.IO.Extensions;
using System.Text;

// Format names:        Binary Resource
// Format references:   binarc.exe
// Format designers:    Sonic Team
// Format researchers:  Radfordhound

namespace Marathon.IO.Types.BINA
{
    public class BINAHeader
    {
        public const int Size = 0x20;

        private const byte _endianFlagLittle = 0x4C; // 'L'
        private const byte _endianFlagBig = 0x42;    // 'B'

        private const string _signature = "BINA";

        public long Offset { get; set; }

        public uint Length { get; set; }

        public uint RelocTableOffset { get; set; }

        public uint RelocTableLength { get; set; }

        public uint ChunkCount { get; set; }

        public uint Version { get; set; }

        public bool IsBigEndian { get; set; } = true;

        public bool HasSignature { get; set; } = true;

        public BINAHeader(uint in_version = 1, bool in_isBigEndian = true)
        {
            Version = in_version;
            IsBigEndian = in_isBigEndian;
        }

        public BINAHeader(BinaryObjectReaderEx in_reader)
        {
            IsBigEndian = true;

            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Offset = in_reader.Position;

            // Jump to signature.
            in_reader.JumpAhead(0x14);

            // Get version string and endianness.
            var flags = in_reader.ReadLittle<uint>();
            var version = "!!!";
            
            IsBigEndian = (byte)((flags & 0xFF000000) >> 24) == _endianFlagBig;

            in_reader.Endianness = IsBigEndian ? Endianness.Big : Endianness.Little;

            unsafe
            {
                // Get version string chars.
                fixed (char* pVersion = version)
                {
                    pVersion[0] = (char)((flags & 0xFF0000) >> 16);
                    pVersion[1] = (char)((flags & 0xFF00) >> 16);
                    pVersion[2] = (char)(flags & 0xFF);
                }
            }

            if (!uint.TryParse(version, out var out_version))
                Logger.Warning($"Unexpected BINA version: {out_version}");

            Version = out_version;

            // Jump to the beginning of the header to read it with the correct endianness.
            in_reader.JumpTo(Offset);

            Length = in_reader.Read<uint>();
            RelocTableOffset = in_reader.Read<uint>();
            RelocTableLength = in_reader.Read<uint>();

            // TODO: unknown.
            var unkField1 = in_reader.Read<uint>();

            if (unkField1 != 0)
                Logger.Warning($"{nameof(unkField1)} is non-zero: {unkField1}");

            ChunkCount = in_reader.Read<uint>();

            if (ChunkCount != 0)
                Logger.Warning($"{nameof(ChunkCount)} is non-zero: {ChunkCount}");

            in_reader.JumpAhead(4);

            HasSignature = in_reader.CheckSignature(_signature, false);

            // TODO: unknown.
            var unkField2 = in_reader.Read<uint>();

            if (unkField2 != 0)
                Logger.Warning($"{nameof(unkField2)} is non-zero: {unkField2}");
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            IsBigEndian = in_writer.Endianness == Endianness.Big;

            in_writer.Write(Length);
            in_writer.Write(RelocTableOffset);
            in_writer.Write(RelocTableLength);
            in_writer.WriteZero<int>();
            in_writer.Write(ChunkCount);

            var version = Version.ToString();

            if (version.Length < 3)
                in_writer.WriteZero<byte>(3 - version.Length);

            in_writer.WriteStringFixedLength(Encoding.UTF8, version, version.Length);
            in_writer.Write(IsBigEndian ? _endianFlagBig : _endianFlagLittle);

            if (HasSignature)
            {
                in_writer.WriteStringFixedLength(Encoding.UTF8, _signature, 4);
            }
            else
            {
                in_writer.WriteZero<int>();
            }

            // TODO: unknown.
            in_writer.WriteZero<int>();
        }
    }
}
