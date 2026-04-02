using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseElement : IBinarySerializableEx
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Type
        {
            get => Value;
            set => Value = value;
        }

        public IFile File { get; set; }

        public KynapseElement? Parent { get; set; } = null;

        public List<KynapseElement> Children { get; set; } = [];

        public KynapseElement() { }

        public KynapseElement(string in_name, object in_value)
        {
            Name = in_name;

            if (in_value is float out_float)
            {
                Value = out_float.ToString("0.0###############f");
            }
            else if (in_value is double out_double)
            {
                Value = out_double.ToString("0.0###############");
            }
            else
            {
                Value = in_value.ToString();
            }
        }

        public KynapseElement(IFile in_file)
        {
            File = in_file;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var type = in_reader.Read<KynapseElementType>();
            var length = in_reader.Read<int>();

            if (length != 0 && type != KynapseElementType.RawData)
                Name = in_reader.ReadStringFixedLength(length);

            switch (type)
            {
                case KynapseElementType.Folder:
                {
                    Value = in_reader.ReadString(StringBinaryFormat.PrefixedLength32);
                    Children = [];

                    var childCount = in_reader.Read<uint>();

                    for (int i = 0; i < childCount; i++)
                    {
                        var element = new KynapseElement()
                        {
                            Parent = this
                        };

                        element.Read(in_reader);

                        Children.Add(element);
                    }

                    break;
                }

                case KynapseElementType.RawData:
                    File = new VirtualFile(GetRawDataFileName(), new SubStream(in_reader.GetBaseStream(), length));
                    in_reader.JumpAhead(length);
                    break;

                case KynapseElementType.Leaf:
                    Value = in_reader.ReadString(StringBinaryFormat.PrefixedLength32);
                    break;
            }
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var type = GetElementType();

            in_writer.Write(type);

            if (type is KynapseElementType.Folder or KynapseElementType.Leaf)
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
            else if (type == KynapseElementType.RawData)
            {
                in_writer.Write((uint)File.Length);
                File.Open().CopyTo(in_writer.GetBaseStream());
            }

            if (type == KynapseElementType.Folder)
            {
                if (!string.IsNullOrEmpty(Value))
                {
                    in_writer.Write(Value.Length);
                    in_writer.WriteStringFixedLength(Value, Value.Length);
                }

                in_writer.Write(Children.Count);

                foreach (var child in Children)
                    child.Write(in_writer);
            }
            else if (type == KynapseElementType.Leaf)
            {
                in_writer.Write(Value.Length);
                in_writer.WriteStringFixedLength(Value, Value.Length);
            }
        }

        public void AddChild(KynapseElement in_element)
        {
            in_element.Parent = this;

            Children.Add(in_element);
        }

        public KynapseElementType GetElementType()
        {
            KynapseElementType result;

            if (Children.Count > 0)
            {
                result = KynapseElementType.Folder;
            }
            else if (File != null)
            {
                result = KynapseElementType.RawData;
            }
            else if (!string.IsNullOrEmpty(Value))
            {
                result = KynapseElementType.Leaf;
            }
            else
            {
                throw new AggregateException("Failed to determine Kynapse element type.");
            }

            return result;
        }

        public string GetRawDataFileName()
        {
            var result = Parent?.Name;

            if (Parent?.Type == "AdditionalData")
                result = Parent?.Parent?.Name;

            return result + GetRawDataExtension();
        }

        public string GetRawDataTypeName()
        {
            var result = Parent?.Type;

            if (result == "AdditionalData")
                return Parent?.Children.FirstOrDefault(x => x.Name == "Class")?.Value;

            return result;
        }

        public Type GetRawDataType()
        {
            return GetRawDataTypeName() switch
            {
                "PathWay" => typeof(KynogonPathWay),
                "CAstarData" => typeof(KynogonAstarData),
                "CFindNearestData" => typeof(KynogonFindNearestData),
                "CPathCostData" => typeof(KynogonPathCostData),
                _ => null
            };
        }

        public string GetRawDataExtension()
        {
            var result = ".bin";

            switch (GetRawDataTypeName())
            {
                case "Mesh":
                    result = ".aim";
                    break;

                case "PathWay":
                    result = FileTypeRegistry.GetAttribute<KynogonPathWay>().GetExtension();
                    break;

                case "Graph":
                    result = ".pdl";
                    break;

                case "CAstarData":
                    result = FileTypeRegistry.GetAttribute<KynogonAstarData>().GetExtension();
                    break;

                case "CFindNearestData":
                    result = FileTypeRegistry.GetAttribute<KynogonFindNearestData>().GetExtension();
                    break;

                case "CPathCostData":
                    result = FileTypeRegistry.GetAttribute<KynogonPathCostData>().GetExtension();
                    break;
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

            return string.Join(System.IO.Path.DirectorySeparatorChar, result);
        }

        public override string ToString()
        {
            var isNameNull = string.IsNullOrEmpty(Name);
            var isValueNull = string.IsNullOrEmpty(Value);

            if (!isNameNull && !isValueNull)
                return $"{Name} = {Value}";

            if (!isNameNull)
                return Name;

            if (!isValueNull)
                return Value;

            return base.ToString();
        }
    }
}
