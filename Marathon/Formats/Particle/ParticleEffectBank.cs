using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

// Format names:        Particle Effect Bank
// Format references:   Sonicteam::GE1PE::EffectBank
// Format designers:    Sonic Team, SEGA Global Entertainment R&D Dept. #1
// Format researchers:  Knuxfan24, Hyper, GordinRamsay

namespace Marathon.Formats.Particle
{
    /// <summary>
    /// Support for *.peb files; used for configuring particle effects.
    /// </summary>
    public class ParticleEffectBank : FileBase
    {
        private const string _extension = ".peb";  // "Particle Effect Bank"
        private const string _signature  = "BEEP"; // "Particle Effect Effect Bank" (reverse)

        /// <summary>
        /// The name of this effect bank.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The effects in this bank.
        /// </summary>
        public List<ParticleEffect> Effects { get; set; } = [];

        public override string Extension => _extension;

        public ParticleEffect this[int in_index]
        {
            get => Effects[in_index];
            set => Effects[in_index] = value;
        }

        public ParticleEffect this[string in_name]
        {
            get => Effects.Find(x => x.Name == in_name);
        }

        public ParticleEffectBank() { }

        public ParticleEffectBank(string in_path) : base(in_path) { }

        public ParticleEffectBank(Stream in_stream) : base(in_stream) { }

        public ParticleEffectBank(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            BINAReader reader = new(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            // Always null.
            reader.JumpAhead(8);

            var effectCount = reader.Read<uint>();

            Name = reader.ReadStringFixedLength(0x20);

            var effectTableOffset = reader.Read<uint>();

            for (int i = 0; i < effectCount; i++)
            {
                var effect = new ParticleEffect
                {
                    Name = reader.ReadStringFixedLength(0x40)
                };

                var nodeCount = reader.Read<uint>();
                var nodeOffset = reader.Read<uint>();

                var pos = reader.Position;

                reader.JumpTo(BINAHeader.Size + nodeOffset);

                for (int j = 0; j < nodeCount; j++)
                {
                    var node = new ParticleEffectNode
                    {
                        UnknownField1 = reader.Read<uint>(),
                        LifeTime = reader.Read<float>(),
                        Density = reader.Read<float>(),
                        UnknownField2 = reader.Read<float>(),
                        Duration = reader.Read<float>(),
                        Velocity = reader.Read<Vector3>(),
                        YLifeTime = reader.Read<float>(),
                        YMagnitude = reader.Read<float>(),
                        Scale = reader.Read<float>(),
                        RandomSpawnRadius = reader.Read<float>(),
                        UnknownField3 = reader.Read<float>(),
                        UnknownField4 = reader.Read<float>(),
                        UnknownField5 = reader.Read<uint>(),
                        UnknownField6 = reader.Read<uint>(),
                        UnknownField7 = reader.Read<uint>(),
                        MaterialName = reader.ReadStringFixedLength(0x20),
                        TextureBankNameA = reader.ReadStringFixedLength(0x20),
                        TextureNameA = reader.ReadStringFixedLength(0x20),
                        TextureBankNameB = reader.ReadStringFixedLength(0x20),
                        TextureNameB = reader.ReadStringFixedLength(0x20),
                        TextureBankNameC = reader.ReadStringFixedLength(0x20),
                        TextureNameC = reader.ReadStringFixedLength(0x20)
                    };

                    var propertyCount = reader.Read<uint>();
                    var propertyOffset = reader.Read<uint>();

                    if (propertyOffset != 0)
                    {
                        var propertyPos = reader.Position;

                        reader.JumpTo(BINAHeader.Size + propertyOffset);

                        while (reader.Position != (propertyOffset + propertyCount) + BINAHeader.Size)
                        {
                            var property = new ParticleEffectNodeProperty().Read(reader);

                            if (property != null)
                                node.Properties.Add(property);
                        }

                        reader.JumpTo(propertyPos);
                    }

                    effect.Nodes.Add(node);
                }

                reader.JumpTo(pos);

                Effects.Add(effect);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteSignature(_signature);
            writer.WriteZero<byte>(8);
            writer.Write(Effects.Count);
            writer.WriteStringFixedLength(Name, 0x20);

            if (Effects.Count <= 0)
            {
                writer.Write(0);
            }
            else
            {
                writer.Reserve<uint>("EffectTableOffset");
                writer.WriteReserved("EffectTableOffset", (uint)writer.Position - BINAHeader.Size);
            }

            for (int i = 0; i < Effects.Count; i++)
            {
                writer.WriteStringFixedLength(Effects[i].Name, 0x40);
                writer.Write(Effects[i].Nodes.Count);

                if (Effects[i].Nodes.Count <= 0)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.Reserve<uint>($"NodeOffset{i}");
                }
            }

            for (int i = 0; i < Effects.Count; i++)
            {
                writer.WriteReserved($"NodeOffset{i}", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Effects[i].Nodes.Count; j++)
                {
                    var node = Effects[i].Nodes[j];

                    writer.Write(node.UnknownField1);
                    writer.Write(node.LifeTime);
                    writer.Write(node.Density);
                    writer.Write(node.UnknownField2);
                    writer.Write(node.Duration);
                    writer.Write(node.Velocity);
                    writer.Write(node.YLifeTime);
                    writer.Write(node.YMagnitude);
                    writer.Write(node.Scale);
                    writer.Write(node.RandomSpawnRadius);
                    writer.Write(node.UnknownField3);
                    writer.Write(node.UnknownField4);
                    writer.Write(node.UnknownField5);
                    writer.Write(node.UnknownField6);
                    writer.Write(node.UnknownField7);
                    writer.WriteStringFixedLength(node.MaterialName, 0x20);
                    writer.WriteStringFixedLength(node.TextureBankNameA, 0x20);
                    writer.WriteStringFixedLength(node.TextureNameA, 0x20);
                    writer.WriteStringFixedLength(node.TextureBankNameB, 0x20);
                    writer.WriteStringFixedLength(node.TextureNameB, 0x20);
                    writer.WriteStringFixedLength(node.TextureBankNameC, 0x20);
                    writer.WriteStringFixedLength(node.TextureNameC, 0x20);

                    var propertyCount = node.Properties.Sum(x => x.Length());

                    writer.Write(propertyCount);

                    if (propertyCount <= 0)
                    {
                        writer.Write(0);
                    }
                    else
                    {
                        writer.Reserve<uint>($"Node{i}PropertyOffset{j}");
                    }
                }
            }

            for (int i = 0; i < Effects.Count; i++)
            {
                for (int j = 0; j < Effects[i].Nodes.Count; j++)
                {
                    if (Effects[i].Nodes[j].Properties.Sum(x => x.Length()) == 0)
                        continue;

                    writer.WriteReserved($"Node{i}PropertyOffset{j}", (uint)writer.Position - BINAHeader.Size);

                    foreach (var property in Effects[i].Nodes[j].Properties)
                        property.Write(writer);
                }
            }

            writer.FinishWrite();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ParticleEffect
    {
        /// <summary>
        /// The name of this particle effect.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The nodes of this particle effect.
        /// </summary>
        public List<ParticleEffectNode> Nodes { get; set; } = [];

        public ParticleEffect() { }

        public ParticleEffect(string in_name, List<ParticleEffectNode> in_node)
        {
            Name = in_name;
            Nodes = in_node;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ParticleEffectNode
    {
        /// <summary>
        /// TODO: unknown, flags?
        /// </summary>
        public uint UnknownField1 { get; set; }

        /// <summary>
        /// The amount of time this particle persists for.
        /// </summary>
        public float LifeTime { get; set; }

        /// <summary>
        /// The amount of particles created from this effect.
        /// </summary>
        public float Density { get; set; }

        /// <summary>
        /// TODO: unknown, possibly unused? Always zero.
        /// </summary>
        public float UnknownField2 { get; set; }

        /// <summary>
        /// The amount of time this particle takes to finish.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// The speed this particle moves at in each direction.
        /// </summary>
        public Vector3 Velocity { get; set; }

        /// <summary>
        /// The amount of time this particle persists for on the Y axis.
        /// </summary>
        public float YLifeTime { get; set; }

        /// <summary>
        /// The speed this particle moves at on the Y axis.
        /// </summary>
        public float YMagnitude { get; set; }

        /// <summary>
        /// The scale of this particle.
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// The radius in which sub-particles can randomly spawn inside.
        /// </summary>
        public float RandomSpawnRadius { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField3 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public float UnknownField4 { get; set; }

        /// <summary>
        /// TODO: unknown, flags?
        /// </summary>
        public uint UnknownField5 { get; set; }

        /// <summary>
        /// TODO: unknown, flags?
        /// </summary>
        public uint UnknownField6 { get; set; }

        /// <summary>
        /// TODO: unknown, flags?
        /// </summary>
        public uint UnknownField7 { get; set; }

        /// <summary>
        /// The material this particle references from the global settings file.
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// The name of a particle texture bank for one this particle's textures.
        /// </summary>
        public string TextureBankNameA { get; set; }

        /// <summary>
        /// The name of the texture used by this particle from the first particle texture bank.
        /// </summary>
        public string TextureNameA { get; set; }

        /// <summary>
        /// The name of a particle texture bank for one this particle's textures.
        /// </summary>
        public string TextureBankNameB { get; set; }

        /// <summary>
        /// The name of the texture used by this particle from the second particle texture bank.
        /// </summary>
        public string TextureNameB { get; set; }

        /// <summary>
        /// The name of a particle texture bank for one this particle's textures.
        /// </summary>
        public string TextureBankNameC { get; set; }

        /// <summary>
        /// The name of the texture used by this particle from the third particle texture bank.
        /// </summary>
        public string TextureNameC { get; set; }

        /// <summary>
        /// The properties of this particle effect.
        /// </summary>
        public List<ParticleEffectNodeProperty> Properties { get; set; } = [];
    }

    public class ParticleEffectNodeProperty
    {
        public ParticleEffectNodePropertyType? Type { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Int32 { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? Single { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Vector3? Vector3 { get; set; } = null;

        public ParticleEffectNodeProperty() { }

        public ParticleEffectNodeProperty(ParticleEffectNodePropertyType in_type)
        {
            Type = in_type;
        }

        public ParticleEffectNodeProperty(ParticleEffectNodePropertyType in_type, int in_int32)
        {
            if (!in_type.ToString().StartsWith("Int32"))
                throw new ArgumentException("The input type is not an Int32.");

            Type = in_type;
            Int32 = in_int32;
        }

        public ParticleEffectNodeProperty(ParticleEffectNodePropertyType in_type, float in_single)
        {
            if (!in_type.ToString().StartsWith("Single"))
                throw new ArgumentException("The input type is not a Single.");

            Type = in_type;
            Single = in_single;
        }

        public ParticleEffectNodeProperty(ParticleEffectNodePropertyType in_type, Vector3 in_vector3)
        {
            if (!in_type.ToString().StartsWith("Vector3"))
                throw new ArgumentException("The input type is not a Vector3.");

            Type = in_type;
            Vector3 = in_vector3;
        }

        public ParticleEffectNodeProperty(ParticleEffectNodePropertyType in_type, object in_object)
        {
            var objType = in_object.GetType();
            var typeName = in_type.ToString();

            if (objType == typeof(int) && typeName.StartsWith("Int32"))
            {
                Int32 = (int)in_object;
            }
            else if (objType == typeof(float) && typeName.StartsWith("Single"))
            {
                Single = (float)in_object;
            }
            else if (objType == typeof(Vector3) && typeName.StartsWith("Vector3"))
            {
                Vector3 = (Vector3)in_object;
            }
            else
            {
                throw new ArgumentException($"Invalid CLR type or property type: {objType.Name} ({typeName}).");
            }

            Type = in_type;
        }

        public ParticleEffectNodeProperty Read(BinaryObjectReaderEx in_reader)
        {
            Type = (ParticleEffectNodePropertyType)in_reader.ReadUInt32();

            if (Type == null)
                return this;

            var typeName = Enum.GetName(Type.GetType(), Type.Value);

            if (string.IsNullOrEmpty(typeName))
            {
                Logger.Warning($"Unknown type at 0x{in_reader.Position:X}: 0x{Type:X}");
                return this;
            }

            if (typeName.StartsWith("Int32"))
            {
                Int32 = in_reader.Read<int>();
            }
            else if (typeName.StartsWith("Single"))
            {
                Single = in_reader.Read<float>();
            }
            else if (typeName.StartsWith("Vector3"))
            {
                Vector3 = in_reader.Read<Vector3>();
            }

            return this;
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write((uint)Type);

            if (Type == null)
            {
                in_writer.Write(0);
                return;
            }

            var typeName = Enum.GetName(Type.GetType(), Type.Value);

            if (string.IsNullOrEmpty(typeName))
            {
                Logger.Warning($"Unknown type: 0x{Type:X}");
                return;
            }

            if (typeName.StartsWith("Int32"))
            {
                in_writer.Write(Int32.Value);
            }
            else if (typeName.StartsWith("Single"))
            {
                in_writer.Write(Single.Value);
            }
            else if (typeName.StartsWith("Vector3"))
            {
                in_writer.Write(Vector3.Value);
            }
        }

        public int Length()
        {
            var result = 4;
            var typeName = Enum.GetName(Type.GetType(), Type.Value);

            if (string.IsNullOrEmpty(typeName))
            {
                Logger.Warning($"Unknown type: 0x{Type:X}");
                return result;
            }

            if (typeName.StartsWith("Int32"))
            {
                result += 4;
            }
            else if (typeName.StartsWith("Single"))
            {
                result += 4;
            }
            else if (typeName.StartsWith("Vector3"))
            {
                result += 12;
            }

            return result;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ParticleEffectNodePropertyType : uint
    {
        /* TODO: figure out how these different types
                 influence the result of the particles. */

        None_00 = 0x00,
        None_02 = 0x02,
        None_04 = 0x04,
        None_0C = 0x0C,

        Int32_03 = 0x03, // TODO: unknown.
        Int32_0B = 0x0B, // TODO: unknown, keyframe index?
        Int32_16 = 0x16, // TODO: unknown.
        Int32_17 = 0x17, // TODO: unknown.
        Int32_18 = 0x18, // TODO: unknown.
        Int32_19 = 0x19, // TODO: unknown.
        Int32_1A = 0x1A, // TODO: unknown.
        Int32_1B = 0x1B, // TODO: unknown.
        Int32_1C = 0x1C, // TODO: unknown.
        Int32_1D = 0x1D, // TODO: unknown.
        Int32_1E = 0x1E, // TODO: unknown.
        Int32_1F = 0x1F, // TODO: unknown.
        Int32_20 = 0x20, // TODO: unknown.
        Int32_21 = 0x21, // TODO: unknown.
        Int32_22 = 0x22, // TODO: unknown.
        Int32_23 = 0x23, // TODO: unknown.
        Int32_24 = 0x24, // TODO: unknown.
        Int32_25 = 0x25, // TODO: unknown.
        Int32_26 = 0x26, // TODO: unknown.
        Int32_27 = 0x27, // TODO: unknown.
        Int32_28 = 0x28, // TODO: unknown.
        Int32_29 = 0x29, // TODO: unknown.
        Int32_2A = 0x2A, // TODO: unknown.
        Int32_2B = 0x2B, // TODO: unknown.
        Int32_2C = 0x2C, // TODO: unknown.
        Int32_2D = 0x2D, // TODO: unknown.
        Int32_2E = 0x2E, // TODO: unknown.
        Int32_2F = 0x2F, // TODO: unknown.
        Int32_30 = 0x30, // TODO: unknown.
        Int32_31 = 0x31, // TODO: unknown.
        Int32_32 = 0x32, // TODO: unknown.
        Int32_33 = 0x33, // TODO: unknown.
        Int32_34 = 0x34, // TODO: unknown.
        Int32_35 = 0x35, // TODO: unknown.
        Int32_36 = 0x36, // TODO: unknown.
        Int32_37 = 0x37, // TODO: unknown.
        Int32_38 = 0x38, // TODO: unknown.
        Int32_39 = 0x39, // TODO: unknown.
        Int32_3A = 0x3A, // TODO: unknown.
        Int32_3B = 0x3B, // TODO: unknown.
        Int32_3C = 0x3C, // TODO: unknown.
        Int32_3D = 0x3D, // TODO: unknown.
        Int32_3E = 0x3E, // TODO: unknown.
        Int32_3F = 0x3F, // TODO: unknown.

        Single_01     = 0x01, // TODO: unknown, keyframe length?
        Single_Radius = 0x05,
        Single_07     = 0x07, // TODO: unknown, radius?
        Single_09     = 0x09, // TODO: unknown.
        Single_Red    = 0x0D,
        Single_Green  = 0x0E,
        Single_Blue   = 0x0F,
        Single_10     = 0x10, // TODO: unknown.
        Single_15     = 0x15, // TODO: unknown.

        Vector3_Scale    = 0x06,
        Vector3_08       = 0x08, // TODO: unknown.
        Vector3_Rotation = 0x0A,
        Vector3_Red      = 0x11,
        Vector3_Green    = 0x12,
        Vector3_Blue     = 0x13,
        Vector3_Alpha    = 0x14
    }
}
