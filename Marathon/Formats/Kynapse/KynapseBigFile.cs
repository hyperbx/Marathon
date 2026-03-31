using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.Formats.Kynapse.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
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

            Root = new Level(xml.Root).ToKynapseElement();

            var binDirName = Path.GetFileName(FileSystemHelper.TruncateAllExtensions(in_path));
            var binDir = Path.Combine(Path.GetDirectoryName(in_path), binDirName);

            void WalkBinaries(KynapseElement in_element)
            {
                var type = in_element.GetElementType();

                if (type == KynapseElementType.Object)
                {
                    foreach (var property in in_element.Children)
                        WalkBinaries(property);
                }
                else if (type == KynapseElementType.Binary && !string.IsNullOrEmpty(in_element.File))
                {
                    var binFile = Path.Combine(binDir, in_element.File);

                    if (!File.Exists(binFile))
                        throw new FileNotFoundException($"Could not find Kynapse binary: {in_element.File}");

                    in_element.Data = File.ReadAllBytes(binFile);
                }
            }

            WalkBinaries(Root);
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.TruncateAllExtensions(path));

            var dir = Directory.CreateDirectory(in_path);
            var name = Path.GetFileNameWithoutExtension(dir.FullName);

            void ExportBinaries(KynapseElement in_element, string in_hierarchy)
            {
                var type = in_element.GetElementType();

                if (type == KynapseElementType.Object)
                {
                    foreach (var property in in_element.Children)
                        ExportBinaries(property, property.GetHierarchy());
                }
                else if (type == KynapseElementType.Binary)
                {
                    var name = in_element?.Value ?? in_element?.Name;

                    if (name == null)
                    {
                        if (in_element.Parent != null && in_element.Parent.Children.Count > 1)
                        {
                            var thisIndex = in_element.Parent.Children.IndexOf(in_element);

                            if (thisIndex > 0)
                            {
                                var nameObject = in_element.Parent.Children[thisIndex - 1];

                                name = nameObject.Value;
                            }
                        }
                    }

                    if (name == null)
                        name = in_element.Parent?.Value ?? in_element.Parent?.Name;

                    var binDir = Directory.CreateDirectory(Path.Combine(dir.FullName, in_hierarchy));
                    var binFile = Path.Combine(binDir.FullName, $"{name}.bin");

                    if (!in_overwrite)
                        ThrowHelper.ThrowFileExistsException(binFile);

                    File.WriteAllBytes(binFile, in_element.Data);

                    in_element.Parent.File = binFile[(dir.FullName.Length + 1)..];
                }
            }

            ExportBinaries(Root, Root.GetHierarchy());

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(dir.FullName), $"{name}{_extension}.xml"), new Level(Root).ToXElement().ToString());
        }
    }
}
