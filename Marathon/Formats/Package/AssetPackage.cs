namespace Marathon.Formats.Package
{
    /// <summary>
    /// File base for the *.pkg format.
    /// <para>Used in SONIC THE HEDGEHOG for defining specific assets with friendly names.</para>
    /// </summary>
    public class AssetPackage : FileBase
    {
        public AssetPackage() { }

        public AssetPackage(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Categories = JsonDeserialise<List<AssetCategory>>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Categories);

                    break;
                }
            }
        }

        public override string Extension { get; } = ".pkg";

        public List<AssetCategory> Categories { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            // File related header stuff.
            uint fileCount = reader.ReadUInt32();
            uint fileEntriesPos = reader.ReadUInt32();

            // Type related header stuff.
            uint categoryCount = reader.ReadUInt32();
            uint categoryEntriesPos = reader.ReadUInt32();

            // Read categories.
            reader.JumpTo(categoryEntriesPos, true);

            for (int i = 0; i < categoryCount; i++)
            {
                AssetCategory category = new();

                // Store offsets for later.
                uint namePos       = reader.ReadUInt32();
                uint typeFileCount = reader.ReadUInt32();
                uint filesPos      = reader.ReadUInt32();

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Jump to namePos and read null terminated string for category name.
                reader.JumpTo(namePos, true);
                category.Name = reader.ReadNullTerminatedString();

                reader.JumpTo(filesPos, true);

                // Read objects within this category.
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
                    category.Files.Add(file);
                    reader.JumpTo(iteratorPosition);
                }

                Categories.Add(category);
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            // Storage for calculated file count.
            uint filesCount = 0;

            // Calculate what we should set the file count to.
            for (int i = 0; i < Categories.Count; i++)
                for (int c = 0; c < Categories[i].Files.Count; c++)
                    filesCount++;

            // Write file count.
            writer.Write(filesCount);

            // Store offset for file entries.
            writer.AddOffset("fileEntriesPos");

            // Write category count.
            writer.Write(Categories.Count);

            // Store offset for category entries.
            writer.AddOffset("categoryEntriesPos");

            // Fill in types offset just before we write the category data.
            writer.FillOffset("categoryEntriesPos", true);

            // Write category data.
            for (int i = 0; i < Categories.Count; i++)
            {
                writer.AddString($"categoryName{i}", Categories[i].Name);
                writer.Write(Categories[i].Files.Count);
                writer.AddOffset($"categoryFilesOffset{i}");
            }

            // Fill in files offset just before we write the file data.
            writer.FillOffset("fileEntriesPos", true);

            // Storage for number of files written.
            int objectNum = 0;

            // Write file data.
            for (int i = 0; i < Categories.Count; i++)
            {
                writer.FillOffset($"categoryFilesOffset{i}", true);

                for (int f = 0; f < Categories[i].Files.Count; f++)
                {
                    writer.AddString($"friendlyName{objectNum}", Categories[i].Files[f].Name);
                    writer.AddString($"filePath{objectNum}", Categories[i].Files[f].File);

                    objectNum++;
                }
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }

    public class AssetCategory
    {
        public string Name { get; set; }

        public List<AssetFile> Files { get; set; } = [];

        public AssetCategory() { }

        public AssetCategory(string in_name, List<AssetFile> in_files = null)
        {
            Name  = in_name;
            Files = in_files ?? [];
        }

        public override string ToString() => Name;
    }

    public class AssetFile
    {
        public string Name { get; set; }

        public string File { get; set; }

        public AssetFile() { }

        public AssetFile(string in_name, string in_file)
        {
            Name = in_name;
            File = in_file;
        }

        public override string ToString() => Name;
    }
}