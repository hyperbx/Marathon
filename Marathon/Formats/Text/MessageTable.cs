using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Marathon.IO;
using Newtonsoft.Json;

namespace Marathon.Formats.Text
{
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
        public string Placeholder { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the MST format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for storing <a href="https://en.wikipedia.org/wiki/UTF-16">UTF-16</a> text with friendly names and placeholder data.</para>
    /// </summary>
    public class MessageTable : FileBase
    {
        public MessageTable() { }

        public MessageTable(string file)
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

        public const string Signature = "WTXT",
                            Extension = ".mst";

        public class FormatData
        {
            public string Name { get; set; }

            public List<Message> Messages = new();
        }

        public FormatData Data = new();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new(fileStream);

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
                    message.Placeholder = reader.ReadNullTerminatedString(false, placeholderOffset, true);

                // Save text entry into the Entries list.
                Data.Messages.Add(message);

                // Return to the previously stored position.
                reader.JumpTo(pos);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAWriter writer = new(fileStream);

            writer.WriteSignature(Signature);
            writer.AddString("Name", Data.Name);
            writer.Write(Data.Messages.Count);

            // Write offsets and string table names for each entry in this file.
            for (int i = 0; i < Data.Messages.Count; i++)
            {
                writer.AddString($"nameOffset{i}", $"{Data.Messages[i].Name}");
                writer.AddOffset($"textOffset{i}");

                if (Data.Messages[i].Placeholder != null)
                    writer.AddString($"placeholderOffset{i}", $"{Data.Messages[i].Placeholder}");
                else
                    writer.WriteNulls(4);
            }

            // Fill text offsets in with the approriate entry's text value.
            for (int i = 0; i < Data.Messages.Count; i++)
            {
                writer.FillInOffset($"textOffset{i}", true);
                writer.WriteNullTerminatedStringUTF16(Data.Messages[i].Text);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }
}
