namespace Marathon.Formats.Text
{
    /// <summary>
    /// File base for the *.pft format.
    /// <para>Used in SONIC THE HEDGEHOG for defining placeholder images for the <see cref="MessageTable"/> format.</para>
    /// </summary>
    public class PictureFont : FileBase
    {
        public PictureFont() { }

        public PictureFont(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Data = JsonDeserialise<FormatData>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Data);

                    break;
                }
            }
        }

        public override string Signature { get; } = "FNTP";

        public override string Extension { get; } = ".pft";

        public class FormatData
        {
            public string Texture;

            public List<Picture> Entries { get; set; } = [];

            public override string ToString() => Texture;
        }

        public FormatData Data { get; set; } = new();

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

        public Picture() { }

        public Picture(string in_name, ushort in_x, ushort in_y, ushort in_width, ushort in_height)
        {
            Name = in_name;
            X = in_x;
            Y = in_y;
            Width = in_width;
            Height = in_height;
        }

        public override string ToString() => Name;
    }
}