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
    public class DirectDrawMap : FileBase, IDictionary<string, byte[]>
    {
        private const string _signature = "DDM ";              // "DirectDraw Map" (speculatory)
        private const string _fileNameChunkSignature = "DSFN"; // "Directdraw Surface File Name" (speculatory)
        private const string _dataChunkSignature = "DSCK";     // "Directdraw Surface ChunK" (speculatory)

        public Dictionary<string, byte[]> Files { get; set; } = [];

        public ICollection<string> Keys => Files.Keys;

        public ICollection<byte[]> Values => Files.Values;

        public int Count => Files.Count;

        public bool IsReadOnly => false;

        public byte[] this[string in_key]
        {
            get => Files[in_key];
            set => Files[in_key] = value;
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

                Files.Add(fileNames[i], data);
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
                writer.WriteStringNullTerminated(file.Key);

            writer.Align(16);

            writer.WriteReserved(fileNameChunkLength, (uint)(writer.Position - fileNameChunkLength - 4));

            foreach (var file in Files)
            {
                writer.WriteSignature(_dataChunkSignature);
                var dataChunkLength = writer.Reserve<uint>();
                var dataLength = writer.Reserve<uint>();

                writer.Align(16);

                var dataStart = writer.Position;

                writer.WriteBytes(file.Value);
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
                Files.Add(Path.GetFileName(file), File.ReadAllBytes(file));
        }

        public override void Export(string in_path = "")
        {
            if (string.IsNullOrEmpty(in_path))
                in_path = Location;

            var dir = Directory.CreateDirectory(FileSystemHelper.TruncateAllExtensions(in_path));

            foreach (var file in Files)
                File.WriteAllBytes(Path.Combine(dir.FullName, file.Key), file.Value);
        }

        public void Add(string in_key, byte[] in_value)
        {
            Files.Add(in_key, in_value);
        }

        public bool ContainsKey(string in_key)
        {
            return Files.ContainsKey(in_key);
        }

        public bool Remove(string in_key)
        {
            return Files.Remove(in_key);
        }

        public bool TryGetValue(string in_key, out byte[] out_value)
        {
            return Files.TryGetValue(in_key, out out_value);
        }

        public void Add(KeyValuePair<string, byte[]> in_item)
        {
            Files.Add(in_item.Key, in_item.Value);
        }

        public void Clear()
        {
            Files.Clear();
        }

        public bool Contains(KeyValuePair<string, byte[]> in_item)
        {
            return Files.ContainsKey(in_item.Key) && Files.ContainsValue(in_item.Value);
        }

        public void CopyTo(KeyValuePair<string, byte[]>[] in_array, int in_arrayIndex)
        {
            Extensions.CollectionExtensions.CopyTo(this, in_array, in_arrayIndex);
        }

        public bool Remove(KeyValuePair<string, byte[]> in_item)
        {
            return Files.Remove(in_item.Key);
        }

        public IEnumerator<KeyValuePair<string, byte[]>> GetEnumerator()
        {
            return Files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
