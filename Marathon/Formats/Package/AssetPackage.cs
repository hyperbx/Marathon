using System.IO;
using System.Collections.Generic;
using Marathon.IO;

namespace Marathon.Formats.Package
{
    public class AssetType
    {
        public string Name { get; set; }

        public List<AssetFile> Files = new();

        public override string ToString() => Name;
    }

    public class AssetFile
    {
        public string Name { get; set; }

        public string File { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the PKG format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining specific assets with friendly names.</para>
    /// </summary>
    public class AssetPackage : FileBase
    {
        public AssetPackage() { }

        public AssetPackage(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Types = JsonDeserialise<List<AssetType>>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public const string Extension = ".pkg";

        public List<AssetType> Types = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            // File related header stuff.
            uint fileCount = reader.ReadUInt32();
            uint fileEntriesPos = reader.ReadUInt32();

            // Type related header stuff.
            uint typeCount = reader.ReadUInt32();
            uint typeEntriesPos = reader.ReadUInt32();

            // Read Types.
            reader.JumpTo(typeEntriesPos, true);

            for (int i = 0; i < typeCount; i++)
            {
                AssetType type = new();

                // Store offsets for later.
                uint namePos       = reader.ReadUInt32();
                uint typeFileCount = reader.ReadUInt32();
                uint filesPos      = reader.ReadUInt32();

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Jump to namePos and read null terminated string for type name.
                reader.JumpTo(namePos, true);
                type.Name = reader.ReadNullTerminatedString();

                reader.JumpTo(filesPos, true);

                // Read objects within this type.
                for (int f = 0; f < typeFileCount; f++)
                {
                    AssetFile file = new();

                    uint friendlyNamePos = reader.ReadUInt32();
                    uint filePathPos = reader.ReadUInt32();

                    // Jump to offsets and read strings for the file name and path.
                    long iteratorPosition = reader.BaseStream.Position;

                    reader.JumpTo(friendlyNamePos, true);
                    file.Name = reader.ReadNullTerminatedString();

                    reader.JumpTo(filePathPos, true);
                    file.File = reader.ReadNullTerminatedString();

                    // Save file entry and return to the previously stored position.
                    type.Files.Add(file);
                    reader.JumpTo(iteratorPosition);
                }

                Types.Add(type);
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            // Storage for calculated file count.
            uint filesCount = 0;

            // Calculate what we should set the file count to.
            for (int i = 0; i < Types.Count; i++)
                for (int c = 0; c < Types[i].Files.Count; c++)
                    filesCount++;

            // Write file count.
            writer.Write(filesCount);

            // Store offset for file entries.
            writer.AddOffset("fileEntriesPos");

            // Write type count.
            writer.Write(Types.Count);

            // Store offset for type entries.
            writer.AddOffset("typeEntriesPos");

            // Fill in types offset just before we write the type data.
            writer.FillInOffset("typeEntriesPos", true);

            // Write type data.
            for (int i = 0; i < Types.Count; i++)
            {
                writer.AddString($"typeName{i}", Types[i].Name);
                writer.Write(Types[i].Files.Count);
                writer.AddOffset($"typeFilesOffset{i}");
            }

            // Fill in files offset just before we write the file data.
            writer.FillInOffset("fileEntriesPos", true);

            // Storage for number of files written.
            int objectNum = 0;

            // Write file data.
            for (int i = 0; i < Types.Count; i++)
            {
                writer.FillInOffset($"typeFilesOffset{i}", true);

                for (int f = 0; f < Types[i].Files.Count; f++)
                {
                    writer.AddString($"friendlyName{objectNum}", Types[i].Files[f].Name);
                    writer.AddString($"filePath{objectNum}", Types[i].Files[f].File);

                    objectNum++;
                }
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }
}