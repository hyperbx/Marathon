using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.Formats.Kynapse.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public KynapseElement Root { get; set; } = new Level().ToKynapseElement();

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
            var dir = Path.Combine(Path.GetDirectoryName(in_path), FileSystemHelper.TruncateAllExtensions(in_path));

            Root = new Level(xml.Root).ToKynapseElement();

            WalkElements((element, type) =>
            {
                if (type != KynapseElementType.RawData)
                    return true;

                var filePath = Path.Combine(dir, element.GetRawDataFileName());

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Could not find Kynapse binary: {filePath}");

                element.File = new PhysicalFile(filePath);

                return true;
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
                    return true;

                var binFile = Path.Combine(dir.FullName, element.GetRawDataFileName());

                if (!in_overwrite)
                    ThrowHelper.ThrowFileExistsException(binFile);

                if (element.File == null)
                    throw new FileNotFoundException("This Kynapse element has no file data.");

                using (var fs = File.OpenWrite(binFile))
                    element.File.Open().CopyTo(fs);

                return true;
            });

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(dir.FullName), $"{name}{_extension}.xml"), new Level(Root).ToXElement().ToString());
        }

        public void AddPathWay(string in_name, KynogonPathWay in_pathWay, bool in_overwrite = true)
        {
            var services = Root.Children.FirstOrDefault(x => x.Type == "Services")
                ?? throw new InvalidDataException("Invalid Kynapse format.");

            var pathWayManager = services.Children.FirstOrDefault(x => x.Name == "PathWayManager");
            var pathWayExists = false;
            var pathWay = new KynapseElement(in_name, "PathWay");

            if (pathWayManager == null)
            {
                pathWayManager = new KynapseElement("PathWayManager", "Service");

                services.Children.Add(pathWayManager);
            }
            else
            {
                for (int i = 0; i < pathWayManager.Children.Count; i++)
                {
                    if (pathWayManager.Children[i].Name != in_name)
                        continue;

                    if (in_overwrite)
                    {
                        pathWayManager.Children[i] = pathWay;
                    }
                    else
                    {
                        ThrowHelper.ThrowFileExistsException(in_name, false);
                    }

                    pathWayExists = true;

                    break;
                }
            }

            if (!pathWayExists)
                pathWayManager.AddChild(pathWay);

            pathWay.AddChild(new KynapseElement() { File = new VirtualFile(in_name, in_pathWay.Write()) });
        }

        public List<KynogonPathWay> GetPathWays()
        {
            var result = new List<KynogonPathWay>();

            var services = Root.Children.FirstOrDefault(x => x.Type == "Services")
                ?? throw new InvalidDataException("Invalid Kynapse format.");

            var pathWayManager = services.Children.FirstOrDefault(x => x.Name == "PathWayManager");

            if (pathWayManager == null)
                return result;

            foreach (var child in pathWayManager.Children)
            {
                if (child.Type != "PathWay")
                    continue;

                foreach (var subChild in child.Children)
                    result.Add(new KynogonPathWay(subChild.File));
            }

            return result;
        }

        public void WalkElements(KynapseElement in_element, Func<KynapseElement, KynapseElementType, bool> in_action)
        {
            var type = in_element.GetElementType();

            // Stop walking if returned false.
            if (!in_action(in_element, type))
                return;

            if (type != KynapseElementType.Folder)
                return;

            foreach (var child in in_element.Children)
                WalkElements(child, in_action);
        }

        public void WalkElements(Func<KynapseElement, KynapseElementType, bool> in_action)
        {
            WalkElements(Root, in_action);
        }

        public Level GetLevel()
        {
            return new Level(Root);
        }
    }
}
