namespace Marathon.Formats.Particle
{
    /// <summary>
    /// File base for the *.peb format.
    /// <para>Used in SONIC THE HEDGEHOG for defining properties for particle effects.</para>
    /// </summary>
    public class ParticleEffectBank : FileBase
    {
        public ParticleEffectBank() { }

        public ParticleEffectBank(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Data = JsonDeserialise<FormatData>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Data);

                    break;
                }
            }
        }

        public override string Signature { get; } = "BEEP";

        public override string Extension { get; } = ".peb";

        public class FormatData
        {
            public string Name { get; set; }

            public List<ParticleEffect> Effects = [];

            public override string ToString() => Name;
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            reader.JumpAhead(0x8); // Always Null.
            uint EntryCount = reader.ReadUInt32();
            Data.Name = reader.ReadNullPaddedString(0x20);
            uint OffsetTable = reader.ReadUInt32();

            reader.JumpTo(OffsetTable, true); // Should already be here but just to be safe.

            // Particle Effect Entries.
            for (int i = 0; i < EntryCount; i++)
            {
                ParticleEffect particle = new();

                particle.Name = reader.ReadNullPaddedString(0x40);
                uint effectCount = reader.ReadUInt32();
                uint effectsOffset = reader.ReadUInt32();

                long position = reader.BaseStream.Position;

                reader.JumpTo(effectsOffset, true);
                for (int e = 0; e < effectCount; e++)
                {
                    ParticleEffectAttributes peAttributes = new();

                    peAttributes.UnknownUInt32_1   = reader.ReadUInt32();
                    peAttributes.Lifetime          = reader.ReadSingle();
                    peAttributes.Density           = reader.ReadSingle();
                    peAttributes.UnknownSingle_1   = reader.ReadSingle();
                    peAttributes.Duration          = reader.ReadSingle();
                    peAttributes.XSpeed            = reader.ReadSingle();
                    peAttributes.YSpeed            = reader.ReadSingle();
                    peAttributes.ZSpeed            = reader.ReadSingle();
                    peAttributes.YLifetime         = reader.ReadSingle();
                    peAttributes.YMagnitude        = reader.ReadSingle();
                    peAttributes.Scale             = reader.ReadSingle();
                    peAttributes.RandomSpawnRadius = reader.ReadSingle();
                    peAttributes.UnknownSingle_5   = reader.ReadSingle();
                    peAttributes.UnknownSingle_6   = reader.ReadSingle();
                    peAttributes.UnknownUInt32_2   = reader.ReadUInt32();
                    peAttributes.UnknownUInt32_3   = reader.ReadUInt32();
                    peAttributes.UnknownUInt32_4   = reader.ReadUInt32();

                    peAttributes.MaterialName       = reader.ReadNullPaddedString(0x20);
                    peAttributes.TextureBankA = reader.ReadNullPaddedString(0x20);
                    peAttributes.TextureNameA     = reader.ReadNullPaddedString(0x20);
                    peAttributes.TextureBankB = reader.ReadNullPaddedString(0x20);
                    peAttributes.TextureNameB     = reader.ReadNullPaddedString(0x20);
                    peAttributes.TextureBankC = reader.ReadNullPaddedString(0x20);
                    peAttributes.TextureNameC     = reader.ReadNullPaddedString(0x20);

                    int PropertyListLength  = reader.ReadInt32();
                    uint PropertyListOffset = reader.ReadUInt32();

                    if (PropertyListOffset != 0)
                    {
                        long currentPos = reader.BaseStream.Position;

                        reader.JumpTo(PropertyListOffset, true);
                        {
                            while (reader.BaseStream.Position != (PropertyListOffset + PropertyListLength) + 0x20)
                            {
                                var property = new ParticleEffectProperty().Read(reader);

                                if (property != null)
                                    peAttributes.Properties.Add(property);
                            }
                        }
                        reader.JumpTo(currentPos);
                    }

                    particle.Attributes.Add(peAttributes);
                }

                reader.JumpTo(position);
                Data.Effects.Add(particle);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);

            writer.WriteNulls(0x08);
            writer.Write(Data.Effects.Count);
            writer.WriteNullPaddedString(Data.Name, 0x20);
            writer.AddOffset("OffsetTable");

            writer.FillOffset("OffsetTable", true);

            for (int i = 0; i < Data.Effects.Count; i++)
            {
                writer.WriteNullPaddedString(Data.Effects[i].Name, 0x40);
                writer.Write(Data.Effects[i].Attributes.Count);
                writer.AddOffset($"EffectsOffset_{i}");
            }

            for (int i = 0; i < Data.Effects.Count; i++)
            {
                writer.FillOffset($"EffectsOffset_{i}", true);

                for (int e = 0; e < Data.Effects[i].Attributes.Count; e++)
                {
                    var currentEffect = Data.Effects[i].Attributes[e];

                    writer.Write(currentEffect.UnknownUInt32_1);
                    writer.Write(currentEffect.Lifetime);
                    writer.Write(currentEffect.Density);
                    writer.Write(currentEffect.UnknownSingle_1);
                    writer.Write(currentEffect.Duration);
                    writer.Write(currentEffect.XSpeed);
                    writer.Write(currentEffect.YSpeed);
                    writer.Write(currentEffect.ZSpeed);
                    writer.Write(currentEffect.YLifetime);
                    writer.Write(currentEffect.YMagnitude);
                    writer.Write(currentEffect.Scale);
                    writer.Write(currentEffect.RandomSpawnRadius);
                    writer.Write(currentEffect.UnknownSingle_5);
                    writer.Write(currentEffect.UnknownSingle_6);
                    writer.Write(currentEffect.UnknownUInt32_2);
                    writer.Write(currentEffect.UnknownUInt32_3);
                    writer.Write(currentEffect.UnknownUInt32_4);

                    writer.WriteNullPaddedString(currentEffect.MaterialName, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureBankA, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureNameA, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureBankB, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureNameB, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureBankC, 0x20);
                    writer.WriteNullPaddedString(currentEffect.TextureNameC, 0x20);

                    int totalProperties = currentEffect.Properties.Sum(x => x.Length());
                    writer.Write(totalProperties);

                    if (totalProperties == 0)
                        writer.Write(0);
                    else
                        writer.AddOffset($"Effects_{i}_PropertyListOffset_{e}");
                }
            }

            for (int i = 0; i < Data.Effects.Count; i++)
            {
                for (int e = 0; e < Data.Effects[i].Attributes.Count; e++)
                {
                    // Don't write offset if null.
                    if (Data.Effects[i].Attributes[e].Properties.Sum(x => x.Length()) == 0)
                        continue;

                    writer.FillOffset($"Effects_{i}_PropertyListOffset_{e}", true);

                    // Write all properties.
                    foreach (var property in Data.Effects[i].Attributes[e].Properties)
                        property.Write(writer);
                }
            }

            writer.FinishWrite();
        }
    }

    public class ParticleEffect
    {
        /// <summary>
        /// Name of this particle effect.
        /// </summary>
        public string Name { get; set; }

        public List<ParticleEffectAttributes> Attributes { get; set; } = [];

        public ParticleEffect() { }

        public ParticleEffect(string in_name, List<ParticleEffectAttributes> in_attributes)
        {
            Name = in_name;
            Attributes = in_attributes;
        }

        public override string ToString() => Name;
    }

    public class ParticleEffectAttributes
    {
        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_1 { get; set; }

        /// <summary>
        /// How long this particle lasts.
        /// </summary>
        public float Lifetime { get; set; }

        /// <summary>
        /// The amount of particles cast from this effect.
        /// </summary>
        public float Density { get; set; }

        /// <summary>
        /// Potentially unused? Always 0.
        /// </summary>
        public float UnknownSingle_1 { get; set; }

        /// <summary>
        /// How long this effect lasts.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// How quickly this effect travels along the X Axis.
        /// </summary>
        public float XSpeed { get; set; }

        /// <summary>
        /// How quickly this effect travels along the Y Axis?
        /// </summary>
        public float YSpeed { get; set; }

        /// <summary>
        /// How quickly this effect travels along the Z Axis.
        /// </summary>
        public float ZSpeed { get; set; }

        /// <summary>
        /// Determines the lifetime of particles on the Y axis.
        /// </summary>
        public float YLifetime { get; set; }

        /// <summary>
        /// Determines the magnitude of the particles on the Y axis.
        /// </summary>
        public float YMagnitude { get; set; }

        /// <summary>
        /// How large this effect is? Might be affected by the flags?
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// The radius in which sub-particles can randomly spawn inside.
        /// </summary>
        public float RandomSpawnRadius { get; set; }

        public float UnknownSingle_5 { get; set; }

        public float UnknownSingle_6 { get; set; }

        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_2 { get; set; }

        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_3 { get; set; }

        /// <summary>
        /// TODO: Unknown - flags?
        /// </summary>
        public uint UnknownUInt32_4 { get; set; }

        /// <summary>
        /// The material to use in sonicnext.pgs.
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// The <see cref="ParticleTextureBank"/> file to pull from for the first texture.
        /// </summary>
        public string TextureBankA { get; set; }

        /// <summary>
        /// The texture entry to look for as the first texture.
        /// </summary>
        public string TextureNameA { get; set; }

        /// <summary>
        /// The <see cref="ParticleTextureBank"/> file to pull from for the second texture.
        /// </summary>
        public string TextureBankB { get; set; }

        /// <summary>
        /// The texture entry to look for as the second texture.
        /// </summary>
        public string TextureNameB { get; set; }

        /// <summary>
        /// The <see cref="ParticleTextureBank"/> to pull from for the third texture.
        /// </summary>
        public string TextureBankC { get; set; }

        /// <summary>
        /// The texture entry to look for as the third texture.
        /// </summary>
        public string TextureNameC { get; set; }

        /// <summary>
        /// A collection of properties that affect this particle.
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

        public ParticleEffectProperty(ParticleEffectPropertyType in_type, int in_int32)
        {
            Type = in_type;
            Int32 = in_int32;
        }

        public ParticleEffectProperty(ParticleEffectPropertyType in_type, float in_single)
        {
            Type = in_type;
            Single = in_single;
        }

        public ParticleEffectProperty(ParticleEffectPropertyType in_type, Vector3 in_vector3)
        {
            Type = in_type;
            Vector3 = in_vector3;
        }

        public ParticleEffectProperty Read(BinaryReaderEx reader)
        {
            Type = (ParticleEffectPropertyType)reader.ReadUInt32();

            if (Type != null)
            {
                string currentType = Enum.GetName(Type.Value);

                if (!string.IsNullOrEmpty(currentType))
                {
                    if (currentType.StartsWith("Int32"))
                    {
                        Int32 = reader.ReadInt32();
                    }
                    else if (currentType.StartsWith("Single"))
                    {
                        Single = reader.ReadSingle();
                    }
                    else if (currentType.StartsWith("Vector3"))
                    {
                        Vector3 = reader.ReadVector3();
                    }
                }
                else
                {
                    Logger.Log($"Unknown type at 0x{reader.BaseStream.Position:X}: 0x{Type:X}");
                }
            }

            return this;
        }

        public void Write(BinaryWriterEx writer)
        {
            writer.Write((uint)Type);

            if (Type != null)
            {
                string currentType = Enum.GetName(Type.Value);

                if (!string.IsNullOrEmpty(currentType))
                {
                    if (currentType.StartsWith("Int32"))
                    {
                        writer.Write(Int32.Value);
                    }
                    else if (currentType.StartsWith("Single"))
                    {
                        writer.Write(Single.Value);
                    }
                    else if (currentType.StartsWith("Vector3"))
                    {
                        writer.Write(Vector3.Value);
                    }
                }
                else
                {
                    Logger.Log($"Unknown type: 0x{Type:X}");
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public int Length()
        {
            string currentType = Enum.GetName(Type.Value);

            int length = 4;

            if (!string.IsNullOrEmpty(currentType))
            {
                if (currentType.StartsWith("Int32"))
                {
                    length += 4;
                }
                else if (currentType.StartsWith("Single"))
                {
                    length += 4;
                }
                else if (currentType.StartsWith("Vector3"))
                {
                    length += 12;
                }
            }
            else
            {
                Logger.Log($"Unknown type: 0x{Type:X}");
            }

            return length;
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
        Int32_0B = 0x0B, // TODO: unknown - keyframe index?
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

        Single_01     = 0x01, // TODO: unknown - keyframe length?
        Single_Radius = 0x05,
        Single_07     = 0x07, // TODO: unknown - radius?
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
