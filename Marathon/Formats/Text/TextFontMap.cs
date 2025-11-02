using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;

// Format names:        Text Font Map
// Format references:   Sonicteam::TextFontMap
// Format designers:    Sonic Team
// Format researchers:  Hyper, Skyth

namespace Marathon.Formats.Text
{
    /// <summary>
    /// Support for *.ftm files; used for storing font character information.
    /// </summary>
    public class TextFontMap : FileBase
    {
        private const string _extension = ".ftm"; // "FonT Map"
        private const string _signature = "FNTM"; // "FoNT Map"
        private const int _maxCodePages = 256;

        /// <summary>
        /// The name of this font map.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of columns in the font atlas.
        /// <para>This field is not used by the game.</para>
        /// </summary>
        public byte Columns { get; set; }

        /// <summary>
        /// The number of rows in the font atlas.
        /// <para>This field is not used by the game.</para>
        /// </summary>
        public byte Rows { get; set; }

        /// <summary>
        /// The pages of character definitions in this font map in Unicode order.
        /// </summary>
        public TextFontMapCodePage[] CodePages { get; set; }

        public override string Extension => _extension;

        public TextFontMap() { }

        public TextFontMap(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream, Endianness);

            reader.CheckSignature(_signature);

            var codePageCount = reader.Read<uint>();
            var codePageTableOffset = reader.Read<uint>();
            Columns = reader.Read<byte>();
            Rows = reader.Read<byte>();
            var unkField = reader.Read<ushort>(); // TODO: unknown - always 1.
            var fontNameOffset = reader.Read<uint>();

            reader.ReadAtOffset(BINAHeader.Size + fontNameOffset,
                () => Name = reader.ReadStringNullTerminated());

            CodePages = new TextFontMapCodePage[codePageCount + 1];

            for (int i = 0; i < CodePages.Length; i++)
            {
                var codePageOffset = reader.Read<uint>();

                if (codePageOffset == 0)
                    continue;

                reader.ReadAtOffset(BINAHeader.Size + codePageOffset, () =>
                {
                    var codePage = new TextFontMapCodePage();

                    for (int j = 0; j < TextFontMapCodePage.Size / 4; j++)
                    {
                        var character = reader.ReadObject<TextFontMapCharacter>();

                        character.Character = (char)((i << 8) | j);

                        codePage.Characters.Add(character);
                    }

                    CodePages[i] = codePage;
                });
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteSignature(_signature);
            writer.Write(CodePages.Length - 1);
            var codePageTableOffset = writer.Reserve<uint>();
            writer.Write(Columns);
            writer.Write(Rows);
            writer.Write<ushort>(1); // TODO: unknown - always 1.
            var fontNameOffset = writer.Reserve<uint>();

            writer.WriteReserved(codePageTableOffset, (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < _maxCodePages; i++)
            {
                if (CodePages.Length <= i || CodePages[i] == null)
                {
                    writer.Write(0);
                    continue;
                }

                writer.Reserve<uint>($"CodePageOffset{i}");
            }

            for (int i = 0; i < _maxCodePages; i++)
            {
                if (CodePages.Length <= i || CodePages[i] == null)
                    continue;

                writer.WriteReserved($"CodePageOffset{i}", (uint)writer.Position - BINAHeader.Size);

                foreach (var character in CodePages[i].Characters)
                    writer.WriteObject(character);
            }

            writer.WriteReserved(fontNameOffset, (uint)writer.Position - BINAHeader.Size);
            writer.WriteStringNullTerminated(Name);
            writer.Align(4);
            writer.FinishWrite();
        }

        public static TextFontMapCodePage CreateCodePage(int in_index)
        {
            var result = new TextFontMapCodePage();

            for (int i = 0; i < TextFontMapCodePage.Size / 4; i++)
            {
                var character = new TextFontMapCharacter
                {
                    Character = (char)((in_index << 8) | i)
                };

                result.Characters.Add(character);
            }

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class TextFontMapCodePage
    {
        public const int Size = 0x400;

        public List<TextFontMapCharacter> Characters { get; set; } = [];
    }

    public class TextFontMapCharacter : IBinarySerializable
    {
        public char Character { get; internal set; }

        public byte Column { get; set; }

        public byte Row { get; set; }

        public ushort Flags { get; set; } = 0xFFFF;

        public void Read(BinaryObjectReader in_reader)
        {
            Column = in_reader.Read<byte>();
            Row = in_reader.Read<byte>();
            Flags = in_reader.Read<ushort>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(Column);
            in_writer.Write(Row);
            in_writer.Write(Flags);
        }

        public override string ToString()
        {
            return Character.ToString();
        }
    }
}