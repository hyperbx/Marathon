using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Format names:        Package
// Format references:   Sonicteam::PackageBinary
// Format designers:    Sonic Team
// Format researchers:  Radfordhound

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for *.pkg files; used for preloading specific assets with friendly names.
    /// </summary>
    public class Package : FileBase, IList<PackageCategory>
    {
        private const string _extension = ".pkg"; // "PacKaGe"

        public List<PackageCategory> Categories { get; set; } = [];

        public int Count => Categories.Count;

        public bool IsReadOnly => false;

        public PackageCategory this[int in_index]
        {
            get => Categories[in_index];
            set => Categories[in_index] = value;
        }

        public PackageCategory this[string in_name]
        {
            get => Categories.Find(x => x.Name == in_name);
        }

        public Package() { }

        public Package(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

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
            var writer = new BINAWriter(in_stream, Endianness);

            writer.Write(GetTotalFileCount());
            writer.Reserve<uint>("FileTableOffset");
            writer.Write(Categories.Count);
            writer.Reserve<uint>("CategoryTableOffset");
            writer.WriteReserved("CategoryTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Categories.Count; i++)
            {
                writer.WriteStringOffset(Categories[i].Name);
                writer.Write(Categories[i].Files.Count);
                writer.Reserve<uint>($"CategoryFileOffset{i}");
            }

            writer.WriteReserved("FileTableOffset", (uint)writer.Position - BINAHeader.Size);

            var fileCount = 0;

            for (int i = 0; i < Categories.Count; i++)
            {
                writer.WriteReserved($"CategoryFileOffset{i}", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Categories[i].Files.Count; j++)
                {
                    writer.WriteStringOffset(Categories[i].Files[j].Name);
                    writer.WriteStringOffset(Categories[i].Files[j].Location);

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

        public int IndexOf(PackageCategory in_item)
        {
            return Categories.IndexOf(in_item);
        }

        public void Insert(int in_index, PackageCategory in_item)
        {
            Categories.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Categories.RemoveAt(in_index);
        }

        public void Add(PackageCategory in_item)
        {
            Categories.Add(in_item);
        }

        public void Clear()
        {
            Categories.Clear();
        }

        public bool Contains(PackageCategory in_item)
        {
            return Categories.Contains(in_item);
        }

        public void CopyTo(PackageCategory[] in_array, int in_arrayIndex)
        {
            Categories.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(PackageCategory in_item)
        {
            return Categories.Remove(in_item);
        }

        public IEnumerator<PackageCategory> GetEnumerator()
        {
            return Categories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PackageCategory : IList<PackageFile>
    {
        public string Name { get; set; }

        public List<PackageFile> Files { get; set; } = [];

        public int Count => Files.Count;

        public bool IsReadOnly => false;

        public PackageFile this[int in_index]
        {
            get => Files[in_index];
            set => Files[in_index] = value;
        }

        public PackageFile this[string in_name]
        {
            get => Files.Find(x => x.Name == in_name);
        }

        public PackageCategory() { }

        public PackageCategory(string in_name, List<PackageFile> in_files = null)
        {
            Name = in_name;
            Files = in_files ?? [];
        }

        public int IndexOf(PackageFile in_item)
        {
            return Files.IndexOf(in_item);
        }

        public void Insert(int in_index, PackageFile in_item)
        {
            Files.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Files.RemoveAt(in_index);
        }

        public void Add(PackageFile in_item)
        {
            Files.Add(in_item);
        }

        public void Clear()
        {
            Files.Clear();
        }

        public bool Contains(PackageFile in_item)
        {
            return Files.Contains(in_item);
        }

        public void CopyTo(PackageFile[] in_array, int in_arrayIndex)
        {
            Files.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(PackageFile in_item)
        {
            return Files.Remove(in_item);
        }

        public IEnumerator<PackageFile> GetEnumerator()
        {
            return Files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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