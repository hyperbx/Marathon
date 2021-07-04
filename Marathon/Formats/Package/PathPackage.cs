using System.IO;
using System.Collections.Generic;
using Marathon.IO;
using Newtonsoft.Json;

namespace Marathon.Formats.Package
{
    public class PathObject
    {
        public string Name { get; set; }

        public string XNO { get; set; }

        public string XNM { get; set; }

        public string TXT { get; set; }

        public string XNV { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the PathObj.bin format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining valid objects for common_path_obj.</para>
    /// </summary>
    public class PathPackage : FileBase
    {
        public PathPackage() { }

        public PathPackage(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    JsonDeserialise(file);
                    break;

                default:
                    Load(file);
                    break;
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
                    entry.XNO = reader.ReadNullTerminatedString(false, XNOOffset, true);

                if (XNMOffset != 0)
                    entry.XNM = reader.ReadNullTerminatedString(false, XNMOffset, true);

                if (TXTOffset != 0)
                    entry.TXT = reader.ReadNullTerminatedString(false, TXTOffset, true);

                if (XNVOffset != 0)
                    entry.XNV = reader.ReadNullTerminatedString(false, XNVOffset, true);

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
                if (PathObjects[i].XNO != null) writer.AddString($"Entry{i}XNO", PathObjects[i].XNO); else writer.WriteNulls(0x4);
                if (PathObjects[i].XNM != null) writer.AddString($"Entry{i}XNM", PathObjects[i].XNM); else writer.WriteNulls(0x4);
                if (PathObjects[i].TXT != null) writer.AddString($"Entry{i}TXT", PathObjects[i].TXT); else writer.WriteNulls(0x4);
                if (PathObjects[i].XNV != null) writer.AddString($"Entry{i}XNV", PathObjects[i].XNV); else writer.WriteNulls(0x4);
            }

            writer.WriteNulls(4);
            writer.FixPadding(0x10);
            writer.FinishWrite();
        }

        public override void JsonSerialise(string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(PathObjects, Formatting.Indented));
        }

        public override void JsonDeserialise(string filePath)
        {
            PathObjects.AddRange(JsonConvert.DeserializeObject<List<PathObject>>(File.ReadAllText(filePath)));
        }
    }
}
