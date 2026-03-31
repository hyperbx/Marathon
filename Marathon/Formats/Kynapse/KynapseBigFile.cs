using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.Formats.Kynapse.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System;
using System.IO;
using System.Xml.Linq;

// Format names:        Kynapse Big File
// Format references:   KynapseSkel::CBigFileDataReader
// Format designers:    Kynogon
// Format researchers:  Hyper, c08o.prkiua
//
// Format research references:
// - Fable II *.ai_config format for original Kynapse XML schema.

namespace Marathon.Formats.Kynapse
{
    /// <summary>
    /// Support for *.kbf files; used for packing Kynapse configuration and binary data.
    /// </summary>
    [FileType("Kynapse Big File", "Kynapse", _extension)]
    public class KynapseBigFile : FileBase
    {
        private const string _extension = ".kbf";        // "Kynapse Big File"
        private const string _signature = "KS BIG FILE"; // "KynapSe BIG FILE"
        private const int _version = 1;

        public KynapseElement Root { get; set; } = new();

        public override string Extension => _extension;

        public override bool UseTempFile => true;

        public KynapseBigFile() { }

        public KynapseBigFile(string in_path) : base(in_path) { }

        public KynapseBigFile(Stream in_stream) : base(in_stream) { }

        public KynapseBigFile(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            var version = reader.Read<int>();

            if (version != _version)
                throw new InvalidSignatureException(_version, version);

            Root = reader.ReadObjectEx<KynapseElement>();
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(_version);
            writer.WriteObjectEx(Root);
        }

        public override void Import(string in_path)
        {
            ThrowHelper.ThrowFileNotFoundException(in_path);

            var xml = XDocument.Load(in_path);
            var dir = Path.GetDirectoryName(in_path);

            Root = new Level(xml.Root).ToKynapseElement();

            WalkElements((element, type) =>
            {
                if (type != KynapseElementType.RawData)
                    return;

                var filePath = Path.Combine(dir, element.Path);

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Could not find Kynapse binary: {element.Path}");

                element.Path = filePath;
            });
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.TruncateAllExtensions(path));

            var dir = Directory.CreateDirectory(in_path);
            var name = Path.GetFileNameWithoutExtension(dir.FullName);

            WalkElements((element, type) =>
            {
                if (type != KynapseElementType.RawData)
                    return;

                var binName = element.Parent?.Type == "AdditionalData"
                    ? element.Parent?.Parent?.Name
                    : element.Parent?.Name ?? element.Parent?.Type;

                var binFile = Path.Combine(dir.FullName, binName + element.GetRawDataExtension());

                if (!in_overwrite)
                    ThrowHelper.ThrowFileExistsException(binFile);

                if (element.File != null || File.Exists(element.Path))
                {
                    using (var fs = File.OpenWrite(binFile))
                    {
                        element.File ??= new PhysicalFile(element.Path);
                        element.File.Open().CopyTo(fs);
                    }
                }
                else
                {
                    throw new FileNotFoundException("This Kynapse element has no file data.");
                }

                element.Path = binFile;
            });

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(dir.FullName), $"{name}{_extension}.xml"), new Level(Root).ToXElement().ToString());
        }

        public void WalkElements(KynapseElement in_element, Action<KynapseElement, KynapseElementType> in_action)
        {
            var type = in_element.GetElementType();

            in_action(in_element, type);

            if (type != KynapseElementType.Folder)
                return;

            foreach (var child in in_element.Children)
                WalkElements(child, in_action);
        }

        public void WalkElements(Action<KynapseElement, KynapseElementType> in_action)
        {
            WalkElements(Root, in_action);
        }

        public Level GetLevel()
        {
            if (!string.IsNullOrEmpty(Location))
            {
                var dirPath = Path.GetDirectoryName(Location);

                // Resolve file paths.
                WalkElements((element, type) =>
                {
                    if (type != KynapseElementType.RawData || Path.IsPathRooted(element.Path))
                        return;

                    var filePath = Path.Combine(dirPath, element.Path);

                    if (!File.Exists(filePath))
                        throw new FileNotFoundException($"Could not find Kynapse binary: {element.Path}");

                    element.Path = filePath;
                });
            }

            return new Level(Root);
        }
    }
}
