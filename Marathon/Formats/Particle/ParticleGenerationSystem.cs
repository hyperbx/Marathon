﻿namespace Marathon.Formats.Particle
{
    /// <summary>
    /// File base for the *.pgs format.
    /// <para>Used in SONIC THE HEDGEHOG for defining material types for particle effects.</para>
    /// </summary>
    public class ParticleGenerationSystem : FileBase
    {
        public ParticleGenerationSystem() { }

        public ParticleGenerationSystem(string file, bool serialise = false)
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

        public override string Signature { get; } = "SGEP";

        public override string Extension { get; } = ".pgs";

        public class FormatData
        {
            public List<string> EffectBanks { get; set; } = [];

            public List<string> TextureBanks { get; set; } = [];

            public List<ParticleMaterial> Materials { get; set; } = [];
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(0x04, Signature);

            reader.JumpAhead(0x04);
            uint particleEffectBankCount   = reader.ReadUInt32();
            uint particleTextureBankCount  = reader.ReadUInt32();
            uint materialCount             = reader.ReadUInt32();
            uint materialSize              = reader.ReadUInt32();
            uint particleEffectBankOffset  = reader.ReadUInt32();
            uint particleTextureBankOffset = reader.ReadUInt32();
            uint materialOffset            = reader.ReadUInt32();

            reader.JumpTo(particleEffectBankOffset, true);
            for (int i = 0; i < particleEffectBankCount; i++)
                Data.EffectBanks.Add(reader.ReadNullPaddedString(0x20));

            reader.JumpTo(particleTextureBankOffset, true);
            for (int i = 0; i < particleTextureBankCount; i++)
                Data.TextureBanks.Add(reader.ReadNullPaddedString(0x20));

            reader.JumpTo(materialOffset, true);
            for (int i = 0; i < materialCount; i++)
            {
                ParticleMaterial material = new()
                {
                    Name = reader.ReadNullPaddedString(0x20),
                    Properties = reader.ReadNullPaddedString(0x40),
                    BlendMode = (BlendMode)reader.ReadUInt32()
                };

                Data.Materials.Add(material);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);

            writer.WriteNulls(0x04);
            writer.Write(Data.EffectBanks.Count);
            writer.Write(Data.TextureBanks.Count);
            writer.Write(Data.Materials.Count);
            writer.Write(ParticleMaterial.Size);
            writer.AddOffset("particleEffectBankOffset");
            writer.AddOffset("particleTextureBankOffset");
            writer.AddOffset("materialOffset");

            writer.FillOffset("particleEffectBankOffset", true);

            foreach (string effect in Data.EffectBanks)
                writer.WriteNullPaddedString(effect, 0x20);

            writer.FillOffset("particleTextureBankOffset", true);

            foreach (string texture in Data.TextureBanks)
                writer.WriteNullPaddedString(texture, 0x20);

            writer.FillOffset("materialOffset", true);

            foreach (ParticleMaterial material in Data.Materials)
            {
                writer.WriteNullPaddedString(material.Name, 0x20);
                writer.WriteNullPaddedString(material.Properties, 0x40);
                writer.Write((uint)material.BlendMode);
            }

            writer.FinishWrite();
        }
    }

    public enum BlendMode
    {
        Additive,
        Negation,
        Opaque = 3
    }

    public class ParticleMaterial
    {
        /// <summary>
        /// The name of this material.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The properties assigned to this material.
        /// <para>Format: Name,%f,%f</para>
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// The blending mode used for this material.
        /// </summary>
        public BlendMode BlendMode { get; set; }

        /// <summary>
        /// The size of this struct.
        /// <para>0 = disables all particles</para>
        /// <para>64 = full struct size</para>
        /// <para>??? = game crash</para>
        /// </summary>
        public const uint Size = 0x64;

        public ParticleMaterial() { }

        public ParticleMaterial(string in_name, string in_properties, BlendMode in_blendMode)
        {
            Name = in_name;
            Properties = in_properties;
            BlendMode = in_blendMode;
        }

        public override string ToString() => Name;
    }
}
