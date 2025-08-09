using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Format names:        DirectDraw Map (speculatory)
// Format references:   Sonicteam::Spanverse::CustomEssenceTextureDDM
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Archive
{
    /// <summary>
    /// Support for *.ddm files; used for storing DirectDraw Surface textures by name.
    /// </summary>
    public class DirectDrawMap : FileBase, IList<DirectDrawMapFile>
    {
        private const string _signature = "DDM ";              // "DirectDraw Map" (speculatory)
        private const string _fileNameChunkSignature = "DSFN"; // "Directdraw Surface File Name" (speculatory)
        private const string _dataChunkSignature = "DSCK";     // "Directdraw Surface ChunK" (speculatory)

        public List<DirectDrawMapFile> Files { get; set; } = [];

        public int Count => Files.Count;

        public bool IsReadOnly => false;

        public DirectDrawMapFile this[int in_index]
        {
            get => Files[in_index];
            set => Files[in_index] = value;
        }

        public DirectDrawMapFile this[string in_name]
        {
            get => Files.Find(x => x.Name == in_name);
        }

        public DirectDrawMap() { }

        public DirectDrawMap(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            var headerChunkLength = reader.Read<uint>();
            var unkField = reader.Read<ushort>();
            var fileCount = reader.Read<ushort>();

            reader.Align(16);

            reader.CheckSignature(_fileNameChunkSignature);

            reader.Align(16);

            var fileNames = new List<string>();

            for (int i = 0; i < fileCount; i++)
                fileNames.Add(reader.ReadStringNullTerminated());

            reader.Align(16);

            for (int i = 0; i < fileCount; i++)
            {
                reader.CheckSignature(_dataChunkSignature);

                var dataChunkLength = reader.Read<int>();
                var dataLength = reader.Read<int>();

                reader.Align(16);

                var data = reader.ReadBytes(dataLength);

                reader.Align(16);

                Files.Add(new(fileNames[i], data));
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(8);         // Header chunk length.
            writer.Write<ushort>(0); // TODO: unknown - always 0?
            writer.Write((ushort)Files.Count);
            writer.Align(16);

            writer.WriteSignature(_fileNameChunkSignature);
            var fileNameChunkLength = writer.Reserve<uint>();
            writer.Write<ushort>(0); // TODO: unknown - always 0?
            writer.Write((ushort)Files.Count);

            writer.Align(16);

            foreach (var file in Files)
                writer.WriteStringNullTerminated(file.Name);

            writer.Align(16);

            writer.WriteReserved(fileNameChunkLength, (uint)(writer.Position - fileNameChunkLength - 4));

            foreach (var file in Files)
            {
                writer.WriteSignature(_dataChunkSignature);
                var dataChunkLength = writer.Reserve<uint>();
                var dataLength = writer.Reserve<uint>();

                writer.Align(16);

                var dataStart = writer.Position;

                writer.WriteBytes(file.Data);
                writer.WriteReserved(dataLength, (uint)(writer.Position - dataStart));

                writer.Align(16);

                writer.WriteReserved(dataChunkLength, (uint)(writer.Position - dataChunkLength - 4));
            }
        }

        public override void Import(string in_path)
        {
            if (!Directory.Exists(in_path))
                throw new DirectoryNotFoundException($"The specified directory does not exist: {in_path}");

            foreach (var file in Directory.GetFiles(in_path, "*.dds", SearchOption.TopDirectoryOnly))
                Files.Add(new(Path.GetFileName(file), File.ReadAllBytes(file)));
        }

        public override void Export(string in_path = "")
        {
            if (string.IsNullOrEmpty(in_path))
                in_path = Location;

            var dir = Directory.CreateDirectory(FileSystemHelper.TruncateAllExtensions(in_path));

            foreach (var file in Files)
                File.WriteAllBytes(Path.Combine(dir.FullName, file.Name), file.Data);
        }

        public int IndexOf(DirectDrawMapFile in_item)
        {
            return Files.IndexOf(in_item);
        }

        public void Insert(int in_index, DirectDrawMapFile in_item)
        {
            Files.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Files.RemoveAt(in_index);
        }

        public void Add(DirectDrawMapFile in_item)
        {
            Files.Add(in_item);
        }

        public void Clear()
        {
            Files.Clear();
        }

        public bool Contains(DirectDrawMapFile in_item)
        {
            return Files.Contains(in_item);
        }

        public void CopyTo(DirectDrawMapFile[] in_array, int in_arrayIndex)
        {
            Files.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(DirectDrawMapFile in_item)
        {
            return Files.Remove(in_item);
        }

        public IEnumerator<DirectDrawMapFile> GetEnumerator()
        {
            return Files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class DirectDrawMapFile
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public DirectDrawMapFile() { }

        public DirectDrawMapFile(string in_name, byte[] in_data)
        {
            Name = in_name;
            Data = in_data;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
