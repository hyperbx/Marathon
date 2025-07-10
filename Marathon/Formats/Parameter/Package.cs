using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Knuxfan24, Hyper

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for *.pkg files; used for preloading specific assets with friendly names.
    /// </summary>
    public class Package : FileBase
    {
        public Package() { }

        public Package(string in_path) : base(in_path) { }

        public List<PackageCategory> Categories { get; set; } = [];

        public PackageCategory this[string in_name]
        {
            get => Categories.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            var fileCount = reader.Read<uint>();
            var fileTableOffset = reader.Read<uint>();
            var categoryCount = reader.Read<uint>();
            var categoryTableOffset = reader.Read<uint>();

            reader.JumpTo(BINAHeader.Size + categoryTableOffset);

            for (int i = 0; i < categoryCount; i++)
            {
                var category = new PackageCategory();

                var categoryNameOffset = reader.Read<uint>();
                var categoryFileCount = reader.Read<uint>();
                var categoryFileTableOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + categoryNameOffset,
                    () => category.Name = reader.ReadStringNullTerminated());

                var pos = reader.Position;

                reader.JumpTo(BINAHeader.Size + categoryFileTableOffset);

                for (int j = 0; j < categoryFileCount; j++)
                {
                    var file = new PackageFile();

                    var fileNameOffset = reader.Read<uint>();
                    var fileLocationOffset = reader.Read<uint>();

                    reader.ReadAtOffset(BINAHeader.Size + fileNameOffset,
                        () => file.Name = reader.ReadStringNullTerminated());

                    reader.ReadAtOffset(BINAHeader.Size + fileLocationOffset,
                        () => file.Location = reader.ReadStringNullTerminated());

                    category.Files.Add(file);
                }

                Categories.Add(category);

                reader.JumpTo(pos);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            writer.Write(GetTotalFileCount());
            writer.CreateNamedField("FileTableOffset");
            writer.Write(Categories.Count);
            writer.CreateNamedField("CategoryTableOffset");
            writer.WriteNamedField("CategoryTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Categories.Count; i++)
            {
                writer.CreateStringField($"CategoryName{i}", Categories[i].Name);
                writer.Write(Categories[i].Files.Count);
                writer.CreateNamedField($"CategoryFileOffset{i}");
            }

            writer.WriteNamedField("FileTableOffset", (uint)writer.Position - BINAHeader.Size);

            var fileCount = 0;

            for (int i = 0; i < Categories.Count; i++)
            {
                writer.WriteNamedField($"CategoryFileOffset{i}", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Categories[i].Files.Count; j++)
                {
                    writer.CreateStringField($"FileName{fileCount}", Categories[i].Files[j].Name);
                    writer.CreateStringField($"FileLocation{fileCount}", Categories[i].Files[j].Location);

                    fileCount++;
                }
            }

            writer.FinishWrite();
        }

        public int GetTotalFileCount()
        {
            var result = 0;

            foreach (var category in Categories)
                result += category.Files.Count;

            return result;
        }
    }

    public class PackageCategory
    {
        public string Name { get; set; }

        public List<PackageFile> Files { get; set; } = [];

        public PackageCategory() { }

        public PackageCategory(string in_name, List<PackageFile> in_files = null)
        {
            Name = in_name;
            Files = in_files ?? [];
        }

        public PackageFile this[string in_name]
        {
            get => Files.Find((x) => x.Name == in_name);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PackageFile
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public PackageFile() { }

        public PackageFile(string in_name, string in_path)
        {
            Name = in_name;
            Location = in_path;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}