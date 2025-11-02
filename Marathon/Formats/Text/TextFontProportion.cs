using Amicitia.IO.Binary;
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
        /// The width of each character cell in the font atlas.
        /// </summary>
        public ushort CellWidth { get; set; }

        /// <summary>
        /// The amount of characters to skip from the beginning of the code page.
        /// </summary>
        public ushort CodePageSeek { get; set; } = 32;

        /// <summary>
        /// The total size of the code page (including the skipped characters from <see cref="CodePageSeek"/>).
        /// </summary>
        public ushort CodePageLength { get; set; } = 255;

        /// <summary>
        /// The width of each character in the code page.
        /// <para>If a character width is zero, it'll fall back to <see cref="CellWidth"/>.</para>
        /// </summary>
        public TextFontProportionCharacter[] Characters { get; set; }

        public override string Extension => _extension;

        public TextFontProportion() { }

        public TextFontProportion(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness);

            reader.CheckSignature(_signature);

            var dataOffset = reader.Read<uint>();

            Kerning = reader.Read<ushort>();
            CellWidth = reader.Read<ushort>();
            CodePageSeek = reader.Read<ushort>();
            CodePageLength = reader.Read<ushort>();

            reader.JumpTo(dataOffset);

            Characters = new TextFontProportionCharacter[CodePageLength - CodePageSeek + 1];

            for (int i = 0; i < Characters.Length; i++)
            {
                var character = reader.ReadObject<TextFontProportionCharacter>();

                character.Character = (char)(CodePageSeek + i);

                Characters[i] = character;
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness);

            writer.WriteSignature(_signature);

            var dataOffset = writer.Reserve();

            writer.Write(Kerning);
            writer.Write(CellWidth);
            writer.Write(CodePageSeek);
            writer.Write(CodePageLength);

            writer.WriteReserved(dataOffset, (uint)writer.Position);

            for (int i = 0; i < Characters.Length; i++)
                writer.WriteObject(Characters[i]);
        }
    }

    public class TextFontProportionCharacter : IBinarySerializable
    {
        public char Character { get; internal set; }

        public byte Width { get; set; }

        public void Read(BinaryObjectReader in_reader)
        {
            Width = in_reader.Read<byte>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(Width);
        }

        public override string ToString()
        {
            return Character.ToString();
        }
    }
}