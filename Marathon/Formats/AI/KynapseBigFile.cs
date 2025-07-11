using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.IO;
using Marathon.IO.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

// Format research attribution: c08o.prkiua, Hyper

namespace Marathon.Formats.AI
{
    public class KynapseBigFile : FileBase
    {
        private const string _extension = ".kbf";        // "Kynapse Big File"
        private const string _signature = "KS BIG FILE"; // "KynapSe BIG FILE"

        public KynapseBigFile() { }

        public KynapseBigFile(string in_path) : base(in_path) { }

        public uint Version { get; set; }

        public KynapseObject Root { get; set; }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            Version = reader.Read<uint>();
            Root = reader.ReadObject<KynapseObject>();
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(Version);
            writer.WriteObject(Root);
        }

        public class KynapseObject : IBinarySerializable
        {
            public string Name { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string? Type { get; set; } = null;

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string? Value { get; set; } = null;

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public byte[]? Data { get; set; } = null;

            public List<KynapseObject> Properties { get; set; } = [];

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
                        Type = in_reader.ReadString(StringBinaryFormat.PrefixedLength32);

                        var propertyCount = in_reader.Read<uint>();

                        for (int i = 0; i < propertyCount; i++)
                            Properties.Add(in_reader.ReadObject<KynapseObject>());

                        break;
                    }

                    case KynapseDataType.Binary:
                    {
                        Data = in_reader.ReadBytes(length);
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
                var type = KynapseDataType.Unknown;

                if (Properties.Count > 0)
                {
                    type = KynapseDataType.Object;
                }
                else if (Data != null && Data.Length > 0)
                {
                    type = KynapseDataType.Binary;
                }
                else if (!string.IsNullOrEmpty(Value))
                {
                    type = KynapseDataType.Property;
                }
                else
                {
                    throw new AggregateException("Failed to determine object type.");
                }

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
                    if (!string.IsNullOrEmpty(Type))
                    {
                        in_writer.Write(Type.Length);
                        in_writer.WriteStringFixedLength(Type, Type.Length);
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

            public override string ToString()
            {
                if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type))
                    return $"{Name} : {Type}";

                return Name ?? Type;
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
}
