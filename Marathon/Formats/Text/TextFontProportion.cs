using Amicitia.IO.Streams;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.IO;

// Format names:        Text Font Proportion
// Format references:   Sonicteam::TextFontProportion
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Text
{
    /// <summary>
    /// Support for *.pfi files; used for storing font character proportions.
    /// </summary>
    public class TextFontProportion : FileBase
    {
        private const string _extension = ".pfi"; // "Proportion Font Info" (speculatory)
        private const string _signature = "PRFI"; // "PRoportion Font Info" (speculatory)

        /// <summary>
        /// The amount of spacing between each character in a string.
        /// </summary>
        public ushort Kerning { get; set; } = 2;

        /// <summary>
        /// The size of each character cell in the font atlas.
        /// </summary>
        public ushort CellSize { get; set; }

        /// <summary>
        /// The amount of characters to skip from the beginning of the character set.
        /// <para>This follows the <a href="https://en.wikipedia.org/wiki/Shift_JIS#Shift_JIS_byte_map">Shift-JIS</a> specification.</para>
        /// </summary>
        public ushort EncodingSeek { get; set; } = 32;

        /// <summary>
        /// The total size of the character set (including the skipped characters from <see cref="EncodingSeek"/>).
        /// <para>This follows the <a href="https://en.wikipedia.org/wiki/Shift_JIS#Shift_JIS_byte_map">Shift-JIS</a> specification.</para>
        /// </summary>
        public ushort EncodingLength { get; set; } = 255;

        /// <summary>
        /// The width of each character in the order of the character set.
        /// <para>If a character width is zero, it'll fall back to <see cref="CellSize"/>.</para>
        /// <para>This follows the <a href="https://en.wikipedia.org/wiki/Shift_JIS#Shift_JIS_byte_map">Shift-JIS</a> specification.</para>
        /// </summary>
        public byte[] CharacterWidths { get; set; }

        public TextFontProportion() { }

        public TextFontProportion(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness);

            reader.CheckSignature(_signature);

            var dataOffset = reader.Read<uint>();

            Kerning = reader.Read<ushort>();
            CellSize = reader.Read<ushort>();
            EncodingSeek = reader.Read<ushort>();
            EncodingLength = reader.Read<ushort>();

            reader.JumpTo(dataOffset);

            CharacterWidths = new byte[EncodingLength - EncodingSeek + 1];

            for (int i = 0; i < CharacterWidths.Length; i++)
                CharacterWidths[i] = reader.Read<byte>();
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness);

            writer.WriteSignature(_signature);

            var dataOffset = writer.Reserve();

            writer.Write(Kerning);
            writer.Write(CellSize);
            writer.Write(EncodingSeek);
            writer.Write(EncodingLength);

            writer.WriteReserved(dataOffset, (uint)writer.Position);

            for (int i = 0; i < CharacterWidths.Length; i++)
                writer.Write(CharacterWidths[i]);
        }
    }
}