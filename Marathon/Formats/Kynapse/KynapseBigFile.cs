using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

// Format names:        Kynapse Big File
// Format references:   Sonicteam::KynapseSkel::CBigFileDataReader
// Format designers:    Sonic Team, Kynogon
// Format researchers:  c08o.prkiua, Hyper

namespace Marathon.Formats.Kynapse
{
    /// <summary>
    /// Support for *.kbf files; used for packing Kynapse configuration and binary data.
    /// </summary>
    public class KynapseBigFile : FileBase
    {
        private const string _extension = ".kbf";        // "Kynapse Big File"
        private const string _signature = "KS BIG FILE"; // "KynapSe BIG FILE"
        private const int _version = 1;

        public KynapseObject Root { get; set; }

        public override string Extension => _extension;

        public KynapseBigFile() { }

        public KynapseBigFile(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);
            reader.JumpAhead(4); // Version

            Root = reader.ReadObject<KynapseObject>();
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(_version);
            writer.WriteObject(Root);
        }

        public override void Import(string in_path)
        {
            ThrowHelper.ThrowFileNotFoundException(in_path);

            Root = JsonConvert.DeserializeObject<KynapseObject>(File.ReadAllText(in_path));

            var binDirName = Path.GetFileName(FileSystemHelper.TruncateAllExtensions(in_path));
            var binDir = Path.Combine(Path.GetDirectoryName(in_path), binDirName);

            void WalkBinaries(KynapseObject in_object)
            {
                var type = in_object.GetDataType();

                if (type == KynapseDataType.Object)
                {
                    foreach (var property in in_object.Properties)
                        WalkBinaries(property);
                }
                else if (type == KynapseDataType.Binary && !string.IsNullOrEmpty(in_object.File))
                {
                    var binFile = Path.Combine(binDir, in_object.File);

                    if (!File.Exists(binFile))
                        throw new FileNotFoundException($"Could not find Kynapse binary: {in_object.File}");

                    in_object.Data = File.ReadAllBytes(binFile);
                }
            }

            WalkBinaries(Root);
        }

        public override void Export(string in_path = "", bool in_isOverwrite = true)
        {
            if (string.IsNullOrEmpty(in_path))
                in_path = Location;

            if (File.Exists(in_path))
                in_path = FilesystemHelper.TruncateAllExtensions(in_path);

            var dir = Directory.CreateDirectory(in_path);
            var name = Path.GetFileNameWithoutExtension(dir.FullName);

            void ExportBinaries(KynapseObject in_object, string in_hierarchy)
            {
                var type = in_object.GetDataType();

                if (type == KynapseDataType.Object)
                {
                    foreach (var property in in_object.Properties)
                        ExportBinaries(property, property.GetHierarchy());
                }
                else if (type == KynapseDataType.Binary)
                {
                    var name = in_object?.Value ?? in_object?.Name;

                    if (name == null)
                    {
                        // Usually binary files are named by a previous property.
                        // This searches for that property so we can inherit the name from it.
                        if (in_object.Parent != null && in_object.Parent.Properties.Count > 1)
                        {
                            var thisIndex = in_object.Parent.Properties.IndexOf(in_object);

                            if (thisIndex > 0)
                            {
                                var nameObject = in_object.Parent.Properties[thisIndex - 1];

                                name = nameObject.Value;
                            }
                        }
                    }

                    if (name == null)
                        name = in_object.Parent?.Value ?? in_object.Parent?.Name;

                    var binDir = Directory.CreateDirectory(Path.Combine(dir.FullName, in_hierarchy));
                    var binFile = Path.Combine(binDir.FullName, $"{name}.bin");

                    if (!in_isOverwrite)
                        ThrowHelper.ThrowFileExistsException(binFile);

                    File.WriteAllBytes(binFile, in_object.Data);

                    in_object.File = '.' + FileSystemHelper.ConvertPathToUnix(binFile[dir.FullName.Length..]);
                }
            }

            ExportBinaries(Root, Root.GetHierarchy());

            var json = JsonConvert.SerializeObject(Root, Formatting.Indented);

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(dir.FullName), $"{name}{_extension}.json"), json);
        }
    }

    public class KynapseObject : IBinarySerializable
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string File { get; set; }

        [JsonIgnore]
        public byte[] Data { get; set; }

        [JsonIgnore]
        public KynapseObject? Parent { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<KynapseObject>? Properties { get; set; } = null;

        public void Read(BinaryObjectReader in_reader)
        {
            var type = in_reader.Read<KynapseDataType>();
            var length = in_reader.Read<int>();

            if (length != 0 && type != KynapseDataType.Binary)
                Name = in_reader.ReadStringFixedLength(length);

            switch (type)
            {
                case KynapseDataType.Object:
                {
                    Value = in_reader.ReadString(StringBinaryFormat.PrefixedLength32);
                    Properties = [];

                    var propertyCount = in_reader.Read<uint>();

                    for (int i = 0; i < propertyCount; i++)
                    {
                        var @object = in_reader.ReadObject<KynapseObject>();

                        @object.Parent = this;

                        Properties.Add(@object);
                    }

                    break;
                }

                case KynapseDataType.Binary:
                {
                    Data = in_reader.ReadBytes(length);
                    Properties = null;
                    break;
                }

                case KynapseDataType.Property:
                {
                    Value = in_reader.ReadString(StringBinaryFormat.PrefixedLength32);
                    break;
                }
            }
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            var type = GetDataType();

            in_writer.Write(type);

            if (type is KynapseDataType.Object or KynapseDataType.Property)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    in_writer.Write(0);
                }
                else
                {
                    in_writer.Write(Name.Length);
                    in_writer.WriteStringFixedLength(Name, Name.Length);
                }
            }
            else if (type == KynapseDataType.Binary)
            {
                in_writer.Write(Data.Length);
                in_writer.WriteBytes(Data);
            }

            if (type == KynapseDataType.Object)
            {
                if (!string.IsNullOrEmpty(Value))
                {
                    in_writer.Write(Value.Length);
                    in_writer.WriteStringFixedLength(Value, Value.Length);
                }

                in_writer.Write(Properties.Count);

                foreach (var property in Properties)
                    property.Write(in_writer);
            }
            else if (type == KynapseDataType.Property)
            {
                in_writer.Write(Value.Length);
                in_writer.WriteStringFixedLength(Value, Value.Length);
            }
        }

        public KynapseDataType GetDataType()
        {
            KynapseDataType result;

            if (Properties?.Count > 0)
            {
                result = KynapseDataType.Object;
            }
            else if (Data?.Length > 0 || !string.IsNullOrEmpty(File))
            {
                result = KynapseDataType.Binary;
            }
            else if (!string.IsNullOrEmpty(Value))
            {
                result = KynapseDataType.Property;
            }
            else
            {
                throw new AggregateException("Failed to determine Kynapse data type.");
            }

            return result;
        }

        public string GetHierarchy()
        {
            var result = new List<string>();
            var node = Parent;

            while (node != null)
            {
                result.Add(node.Name ?? node.Value);
                node = node.Parent;
            }

            result.Reverse();

            return string.Join('/', result);
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Value))
            {
                var type = GetDataType();
                var delimiter = type == KynapseDataType.Object ? ":" : "=";

                return $"{Name} {delimiter} {Value}";
            }

            return Name ?? Value;
        }
    }

    public enum KynapseDataType : int
    {
        Unknown = -1,
        Object,
        Binary,
        Property
    }
}
