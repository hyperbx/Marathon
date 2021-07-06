using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Text
{
    /// <summary>
    /// The data structure pertaining to each entry for the <see cref="PictureFont"/> format.
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// The name of the picture.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The X position of the top-left corner of the picture.
        /// </summary>
        public ushort X { get; set; }

        /// <summary>
        /// The Y position of the top-left corner of the picture.
        /// </summary>
        public ushort Y { get; set; }

        /// <summary>
        /// The width of the picture.
        /// </summary>
        public ushort Width { get; set; }

        /// <summary>
        /// The height of the picture.
        /// </summary>
        public ushort Height { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the PFT format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining placeholder images for the <see cref="MessageTable"/> format.</para>
    /// </summary>
    public class PictureFont : FileBase
    {
        public PictureFont() { }

        public PictureFont(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Data = JsonDeserialise<FormatData>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public const string Signature = "FNTP",
                            Extension = ".pft";

        public class FormatData
        {
            public string Texture;

            public List<Picture> Entries = new();

            public override string ToString() => Texture;
        }

        public FormatData Data = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            uint texturePos = reader.ReadUInt32();
            uint placeholderEntries = reader.ReadUInt32();

            reader.JumpTo(reader.ReadUInt32(), true);
            long position = reader.BaseStream.Position;

            Data.Texture = reader.ReadNullTerminatedString(false, texturePos, true);

            reader.JumpTo(position, false);
            for (uint i = 0; i < placeholderEntries; i++)
            {
                uint placeholderEntry = reader.ReadUInt32();

                Picture fontPicture = new()
                {
                    X = reader.ReadUInt16(),
                    Y = reader.ReadUInt16(),
                    Width = reader.ReadUInt16(),
                    Height = reader.ReadUInt16()
                };

                position = reader.BaseStream.Position;

                reader.JumpTo(placeholderEntry, true);
                fontPicture.Name = reader.ReadNullTerminatedString();
                reader.JumpTo(position, false);

                Data.Entries.Add(fontPicture);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);

            writer.AddString("textureName", Data.Texture);

            writer.Write((uint)Data.Entries.Count);
            writer.AddOffset("placeholderEntriesPos");
            writer.FillOffset("placeholderEntriesPos", true);

            for (int i = 0; i < Data.Entries.Count; i++)
            {
                writer.AddString($"placeholderName{i}", Data.Entries[i].Name);
                writer.Write(Data.Entries[i].X);
                writer.Write(Data.Entries[i].Y);
                writer.Write(Data.Entries[i].Width);
                writer.Write(Data.Entries[i].Height);
            }

            writer.FinishWrite();
        }
    }
}