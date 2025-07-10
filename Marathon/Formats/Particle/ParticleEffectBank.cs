using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

// Format research attribution: Knuxfan24, Hyper, GordinRamsay

namespace Marathon.Formats.Particle
{
    /// <summary>
    /// Support for *.peb files; used for configuring particle effects.
    /// </summary>
    public class ParticleEffectBank : FileBase
    {
        private const string _signature  = "BEEP"; // "Particle Effect Effect Bank" (reverse)

        public ParticleEffectBank() { }

        public ParticleEffectBank(string in_path) : base(in_path) { }

        public string Name { get; set; }

        public List<ParticleEffect> Effects { get; set; } = [];

        public ParticleEffect this[string in_name]
        {
            get => Effects.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            BINAReader reader = new(in_stream);

            reader.CheckSignature(_signature);

            // Always null.
            reader.JumpAhead(8);

            var entryCount = reader.Read<uint>();

            Name = reader.ReadStringFixedLength(0x20);

            var entryTableOffset = reader.Read<uint>();

            for (int i = 0; i < entryCount; i++)
            {
                var effect = new ParticleEffect
                {
                    Name = reader.ReadStringFixedLength(0x40)
                };

                var effectsCount = reader.Read<uint>();
                var effectsOffset = reader.Read<uint>();

                var pos = reader.Position;

                reader.JumpTo(BINAHeader.Size + effectsOffset);

                for (int j = 0; j < effectsCount; j++)
                {
                    var attr = new ParticleEffectAttributes
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
                        TextureBankA = reader.ReadStringFixedLength(0x20),
                        TextureNameA = reader.ReadStringFixedLength(0x20),
                        TextureBankB = reader.ReadStringFixedLength(0x20),
                        TextureNameB = reader.ReadStringFixedLength(0x20),
                        TextureBankC = reader.ReadStringFixedLength(0x20),
                        TextureNameC = reader.ReadStringFixedLength(0x20)
                    };

                    var propertyListCount = reader.Read<uint>();
                    var propertyListOffset = reader.Read<uint>();

                    if (propertyListOffset != 0)
                    {
                        var propertyListPos = reader.Position;

                        reader.JumpTo(BINAHeader.Size + propertyListOffset);

                        while (reader.Position != (propertyListOffset + propertyListCount) + BINAHeader.Size)
                        {
                            var property = new ParticleEffectProperty().Read(reader);

                            if (property != null)
                                attr.Properties.Add(property);
                        }

                        reader.JumpTo(propertyListPos);
                    }

                    effect.Attributes.Add(attr);
                }

                reader.JumpTo(pos);

                Effects.Add(effect);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            writer.WriteSignature(_signature);
            writer.WriteNullBytes(8);
            writer.Write(Effects.Count);
            writer.WriteStringFixedLength(Name, 0x20);
            writer.CreateNamedField("EntryTableOffset");
            writer.WriteNamedField("EntryTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Effects.Count; i++)
            {
                writer.WriteStringFixedLength(Effects[i].Name, 0x40);
                writer.Write(Effects[i].Attributes.Count);
                writer.CreateNamedField($"EffectsOffset{i}");
            }

            for (int i = 0; i < Effects.Count; i++)
            {
                writer.WriteNamedField($"EffectsOffset{i}", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Effects[i].Attributes.Count; j++)
                {
                    var attr = Effects[i].Attributes[j];

                    writer.Write(attr.UnknownField1);
                    writer.Write(attr.LifeTime);
                    writer.Write(attr.Density);
                    writer.Write(attr.UnknownField2);
                    writer.Write(attr.Duration);
                    writer.Write(attr.Velocity);
                    writer.Write(attr.YLifeTime);
                    writer.Write(attr.YMagnitude);
                    writer.Write(attr.Scale);
                    writer.Write(attr.RandomSpawnRadius);
                    writer.Write(attr.UnknownField3);
                    writer.Write(attr.UnknownField4);
                    writer.Write(attr.UnknownField5);
                    writer.Write(attr.UnknownField6);
                    writer.Write(attr.UnknownField7);

                    writer.WriteStringFixedLength(attr.MaterialName, 0x20);
                    writer.WriteStringFixedLength(attr.TextureBankA, 0x20);
                    writer.WriteStringFixedLength(attr.TextureNameA, 0x20);
                    writer.WriteStringFixedLength(attr.TextureBankB, 0x20);
                    writer.WriteStringFixedLength(attr.TextureNameB, 0x20);
                    writer.WriteStringFixedLength(attr.TextureBankC, 0x20);
                    writer.WriteStringFixedLength(attr.TextureNameC, 0x20);

                    var propertyCount = attr.Properties.Sum(x => x.Length());

                    writer.Write(propertyCount);

                    if (propertyCount == 0)
                    {
                        writer.Write(0);
                    }
                    else
                    {
                        writer.CreateNamedField($"Effects{i}PropertyListOffset{j}");
                    }
                }
            }

            for (int i = 0; i < Effects.Count; i++)
            {
                for (int j = 0; j < Effects[i].Attributes.Count; j++)
                {
                    if (Effects[i].Attributes[j].Properties.Sum(x => x.Length()) == 0)
                        continue;

                    writer.WriteNamedField($"Effects{i}PropertyListOffset{j}", (uint)writer.Position - BINAHeader.Size);

                    foreach (var property in Effects[i].Attributes[j].Properties)
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
        /// The attributes of this particle effect.
        /// </summary>
        public List<ParticleEffectAttributes> Attributes { get; set; } = [];

        public ParticleEffect() { }

        public ParticleEffect(string in_name, List<ParticleEffectAttributes> in_attributes)
        {
            Name = in_name;
            Attributes = in_attributes;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ParticleEffectAttributes
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
        /// The location of a particle texture bank for one this particle's textures.
        /// </summary>
        public string TextureBankA { get; set; }

        /// <summary>
        /// The name of the texture used by this particle from the first particle texture bank.
        /// </summary>
        public string TextureNameA { get; set; }

        /// <summary>
        /// The location of a particle texture bank for one this particle's textures.
        /// </summary>
        public string TextureBankB { get; set; }

        /// <summary>
        /// The name of the texture used by this particle from the second particle texture bank.
        /// </summary>
        public string TextureNameB { get; set; }

        /// <summary>
        /// The location of a particle texture bank for one this particle's textures.
        /// </summary>
        public string TextureBankC { get; set; }

        /// <summary>
        /// The name of the texture used by this particle from the third particle texture bank.
        /// </summary>
        public string TextureNameC { get; set; }

        /// <summary>
        /// The properties of this particle effect.
        /// </summary>
        public List<ParticleEffectProperty> Properties { get; set; } = [];
    }

    public class ParticleEffectProperty
    {
        public ParticleEffectPropertyType? Type { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Int32 { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? Single { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Vector3? Vector3 { get; set; } = null;

        public ParticleEffectProperty() { }

        public ParticleEffectProperty(ParticleEffectPropertyType in_type)
        {
            Type = in_type;
        }

        public ParticleEffectProperty(ParticleEffectPropertyType in_type, int in_int32)
        {
            if (!in_type.ToString().StartsWith("Int32"))
                throw new ArgumentException("The input type is not an Int32.");

            Type = in_type;
            Int32 = in_int32;
        }

        public ParticleEffectProperty(ParticleEffectPropertyType in_type, float in_single)
        {
            if (!in_type.ToString().StartsWith("Single"))
                throw new ArgumentException("The input type is not a Single.");

            Type = in_type;
            Single = in_single;
        }

        public ParticleEffectProperty(ParticleEffectPropertyType in_type, Vector3 in_vector3)
        {
            if (!in_type.ToString().StartsWith("Vector3"))
                throw new ArgumentException("The input type is not a Vector3.");

            Type = in_type;
            Vector3 = in_vector3;
        }

        public ParticleEffectProperty(ParticleEffectPropertyType in_type, object in_object)
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

        public ParticleEffectProperty Read(BinaryObjectReaderEx in_reader)
        {
            Type = (ParticleEffectPropertyType)in_reader.ReadUInt32();

            if (Type == null)
                return this;

            var typeName = Enum.GetName(Type.Value);

            if (string.IsNullOrEmpty(typeName))
            {
                Logger.Log($"Unknown type at 0x{in_reader.Position:X}: 0x{Type:X}");
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

            var typeName = Enum.GetName(Type.Value);

            if (string.IsNullOrEmpty(typeName))
            {
                Logger.Log($"Unknown type: 0x{Type:X}");
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
            var typeName = Enum.GetName(Type.Value);

            if (string.IsNullOrEmpty(typeName))
            {
                Logger.Log($"Unknown type: 0x{Type:X}");
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
    public enum ParticleEffectPropertyType : uint
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
