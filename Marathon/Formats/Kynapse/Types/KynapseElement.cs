using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseElement : IBinarySerializableEx
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Type => Value;

        public string File { get; set; }

        public byte[] Data { get; set; }

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

        public KynapseElement(byte[] in_data)
        {
            Data = in_data;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var type = in_reader.Read<KynapseElementType>();
            var length = in_reader.Read<int>();

            if (length != 0 && type != KynapseElementType.Binary)
                Name = in_reader.ReadStringFixedLength(length);

            switch (type)
            {
                case KynapseElementType.Object:
                {
                    Value = in_reader.ReadString(StringBinaryFormat.PrefixedLength32);
                    Children = [];

                    var childCount = in_reader.Read<uint>();

                    for (int i = 0; i < childCount; i++)
                    {
                        var element = in_reader.ReadObjectEx<KynapseElement>();

                        element.Parent = this;

                        Children.Add(element);
                    }

                    break;
                }

                case KynapseElementType.Binary:
                    Data = in_reader.ReadBytes(length);
                    break;

                case KynapseElementType.Property:
                    Value = in_reader.ReadString(StringBinaryFormat.PrefixedLength32);
                    break;
            }
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var type = GetElementType();

            in_writer.Write(type);

            if (type is KynapseElementType.Object or KynapseElementType.Property)
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
            else if (type == KynapseElementType.Binary)
            {
                in_writer.Write(Data.Length);
                in_writer.WriteBytes(Data);
            }

            if (type == KynapseElementType.Object)
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
            else if (type == KynapseElementType.Property)
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
                result = KynapseElementType.Object;
            }
            else if (Data?.Length > 0 || !string.IsNullOrEmpty(File))
            {
                result = KynapseElementType.Binary;
            }
            else if (!string.IsNullOrEmpty(Value))
            {
                result = KynapseElementType.Property;
            }
            else
            {
                throw new AggregateException("Failed to determine Kynapse element type.");
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

            return string.Join(Path.DirectorySeparatorChar, result);
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
