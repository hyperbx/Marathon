namespace Marathon.Formats.Text
{
    /// <summary>
    /// File base for the *.mst format.
    /// <para>Used in SONIC THE HEDGEHOG for storing <a href="https://en.wikipedia.org/wiki/UTF-16">UTF-16</a> text with friendly names and placeholder data.</para>
    /// </summary>
    public class MessageTable : FileBase
    {
        public MessageTable() { }

        public MessageTable(string file, bool serialise = false)
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

        public override string Signature { get; } = "WTXT";

        public override string Extension { get; } = ".mst";

        public class FormatData
        {
            public string Name { get; set; }

            public List<Message> Messages { get; set; } = new();

            public override string ToString() => Name;
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            // Store offsets for the string table.
            uint stringTableOffset = reader.ReadUInt32(); // Location of the table of internal string names (including the name) in this file.
            uint stringCount       = reader.ReadUInt32(); // Count of internal string enteries in this file.

            // Get name of MST.
            long namePos = reader.BaseStream.Position;
            reader.JumpTo(stringTableOffset, true);
            Data.Name = reader.ReadNullTerminatedString();
            reader.JumpTo(namePos);

            for (int i = 0; i < stringCount; i++)
            {
                Message message = new();

                // Store offsets for later.
                uint nameOffset = reader.ReadUInt32();
                uint textOffset = reader.ReadUInt32();
                uint placeholderOffset = reader.ReadUInt32();

                // Stores the current position to jump back to later.
                long pos = reader.BaseStream.Position;

                // Jump to offsets and read null terminated strings.
                message.Name = reader.ReadNullTerminatedString(false, nameOffset, true);
                message.Text = reader.ReadNullTerminatedString(true, textOffset, true);

                // Not all entries have a placeholder value, entires that lack one have this offset set to zero.
                if (placeholderOffset != 0)
                    message.Placeholders = reader.ReadNullTerminatedString(false, placeholderOffset, true).Split(',');

                // Save text entry into the Entries list.
                Data.Messages.Add(message);

                // Return to the previously stored position.
                reader.JumpTo(pos);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);
            writer.AddString("Name", Data.Name);
            writer.Write(Data.Messages.Count);

            // Write offsets and string table names for each entry in this file.
            for (int i = 0; i < Data.Messages.Count; i++)
            {
                writer.AddString($"nameOffset{i}", $"{Data.Messages[i].Name}");
                writer.AddOffset($"textOffset{i}");

                if (Data.Messages[i].Placeholders != null)
                {
                    writer.AddString($"placeholderOffset{i}", $"{string.Join(',', Data.Messages[i].Placeholders)}");
                }
                else
                {
                    writer.WriteNulls(4);
                }
            }

            // Fill text offsets in with the approriate entry's text value.
            for (int i = 0; i < Data.Messages.Count; i++)
            {
                writer.FillOffset($"textOffset{i}", true);
                writer.WriteNullTerminatedStringUTF16(Data.Messages[i].Text);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }

    public class Message
    {
        /// <summary>
        /// Name of this message.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Text of this message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Placeholder Data (such as voice lines and button icons) for this message.
        /// </summary>
        public string[] Placeholders { get; set; }

        public Message() { }

        public Message(string in_name, string in_text, string[] in_placeholders = null)
        {
            Name = in_name;
            Text = in_text;
            Placeholders = in_placeholders;
        }

        public override string ToString() => Name;
    }
}
