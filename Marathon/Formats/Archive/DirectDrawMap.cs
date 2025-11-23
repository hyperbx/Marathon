using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
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
    public class DirectDrawMap : FileBase
    {
        private const string _extension = ".ddm";              // "DirectDraw Map" (speculatory)
        private const string _signature = "DDM ";              // "DirectDraw Map" (speculatory)
        private const string _fileNameChunkSignature = "DSFN"; // "Directdraw Surface File Name" (speculatory)
        private const string _dataChunkSignature = "DSCK";     // "Directdraw Surface ChunK" (speculatory)

        public Dictionary<string, byte[]> Files { get; set; } = [];

        public override string Extension => _extension;

        public byte[] this[string in_key]
        {
            get => Files[in_key];
            set => Files[in_key] = value;
        }

        public DirectDrawMap() { }

        public DirectDrawMap(string in_path) : base(in_path) { }

        public DirectDrawMap(Stream in_stream) : base(in_stream) { }

        public DirectDrawMap(IFile in_file) : base(in_file) { }

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
            ThrowHelper.ThrowDirectoryNotFoundException(in_path);

            foreach (var file in Directory.GetFiles(in_path, "*.dds", SearchOption.TopDirectoryOnly))
                Files.Add(Path.GetFileName(file), File.ReadAllBytes(file));
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.TruncateAllExtensions(path));

            var dir = Directory.CreateDirectory(in_path);

            foreach (var file in Files)
            {
                var path = Path.Combine(dir.FullName, file.Key);

                if (!in_overwrite)
                    ThrowHelper.ThrowFileExistsException(path);

                File.WriteAllBytes(path, file.Value);
            }
        }
    }
}
