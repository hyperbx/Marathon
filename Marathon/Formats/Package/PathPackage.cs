using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Package
{
    public class PathObject
    {
        /// <summary>
        /// Name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Model this object should use.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Animation this object should use.
        /// </summary>
        public string Animation { get; set; }

        /// <summary>
        /// TODO: Unknown.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Material animation this object should use.
        /// </summary>
        public string MaterialAnimation { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the PathObj.bin format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining valid objects for common_path_obj.</para>
    /// </summary>
    public class PathPackage : FileBase
    {
        public PathPackage() { }

        public PathPackage(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    PathObjects = JsonDeserialise<List<PathObject>>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(PathObjects);

                    break;
                }
            }
        }

        public const string Extension = ".bin";

        public List<PathObject> PathObjects = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            // Read the first entry's name so we can find the string table's position.
            uint stringTablePosition = reader.ReadUInt32();
            reader.JumpBehind(0x4);

            // Loop through and read entries.
            while (reader.BaseStream.Position < (stringTablePosition - 4) + 0x20)
            {
                PathObject entry = new();
                
                // Read string offsets.
                uint EntryNameOffset = reader.ReadUInt32();
                uint XNOOffset = reader.ReadUInt32();
                uint XNMOffset = reader.ReadUInt32();
                uint TXTOffset = reader.ReadUInt32();
                uint XNVOffset = reader.ReadUInt32();
                long position = reader.BaseStream.Position;

                // Read and store all the string values if the offset value wasn't null.
                if (EntryNameOffset != 0)
                    entry.Name = reader.ReadNullTerminatedString(false, EntryNameOffset, true);

                if (XNOOffset != 0)
                    entry.Model = reader.ReadNullTerminatedString(false, XNOOffset, true);

                if (XNMOffset != 0)
                    entry.Animation = reader.ReadNullTerminatedString(false, XNMOffset, true);

                if (TXTOffset != 0)
                    entry.Text = reader.ReadNullTerminatedString(false, TXTOffset, true);

                if (XNVOffset != 0)
                    entry.MaterialAnimation = reader.ReadNullTerminatedString(false, XNVOffset, true);

                // Save entry and jump back to position to read the next entry.
                PathObjects.Add(entry);
                reader.JumpTo(position);
            }
        }
        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            for (int i = 0; i < PathObjects.Count; i++)
            {
                if (PathObjects[i].Name != null) writer.AddString($"Entry{i}Name", PathObjects[i].Name); else writer.WriteNulls(0x4);
                if (PathObjects[i].Model != null) writer.AddString($"Entry{i}XNO", PathObjects[i].Model); else writer.WriteNulls(0x4);
                if (PathObjects[i].Animation != null) writer.AddString($"Entry{i}XNM", PathObjects[i].Animation); else writer.WriteNulls(0x4);
                if (PathObjects[i].Text != null) writer.AddString($"Entry{i}TXT", PathObjects[i].Text); else writer.WriteNulls(0x4);
                if (PathObjects[i].MaterialAnimation != null) writer.AddString($"Entry{i}XNV", PathObjects[i].MaterialAnimation); else writer.WriteNulls(0x4);
            }

            writer.WriteNulls(4);
            writer.FixPadding(0x10);
            writer.FinishWrite();
        }
    }
}
