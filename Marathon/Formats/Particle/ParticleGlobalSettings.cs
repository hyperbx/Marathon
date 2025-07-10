using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Knuxfan24, Hyper

namespace Marathon.Formats.Particle
{
    /// <summary>
    /// Support for *.pgs files; used for global settings for particle effects.
    /// </summary>
    public class ParticleGlobalSettings : FileBase
    {
        private const string _signature = "SGEP"; // "Particle Effect Global Settings" (inverse)

        public ParticleGlobalSettings() { }

        public ParticleGlobalSettings(string in_path) : base(in_path) { }

        public List<string> EffectBanks { get; set; } = [];

        public List<string> TextureBanks { get; set; } = [];

        public List<ParticleMaterial> Materials { get; set; } = [];

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            reader.CheckSignature(_signature);
            reader.JumpAhead(4);

            var particleEffectBankCount = reader.Read<uint>();
            var particleTextureBankCount = reader.Read<uint>();
            var materialCount = reader.Read<uint>();
            var materialSize = reader.Read<uint>();
            var particleEffectBankOffset = reader.Read<uint>();
            var particleTextureBankOffset = reader.Read<uint>();
            var materialOffset = reader.Read<uint>();

            reader.JumpTo(BINAHeader.Size + particleEffectBankOffset);

            for (int i = 0; i < particleEffectBankCount; i++)
                EffectBanks.Add(reader.ReadStringFixedLength(0x20));

            reader.JumpTo(BINAHeader.Size + particleTextureBankOffset);

            for (int i = 0; i < particleTextureBankCount; i++)
                TextureBanks.Add(reader.ReadStringFixedLength(0x20));

            reader.JumpTo(BINAHeader.Size + materialOffset);

            for (int i = 0; i < materialCount; i++)
            {
                var material = new ParticleMaterial()
                {
                    Name = reader.ReadStringFixedLength(0x20),
                    Properties = reader.ReadStringFixedLength(0x40),
                    BlendMode = (ParticleBlendMode)reader.Read<uint>()
                };

                Materials.Add(material);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            writer.WriteSignature(_signature);
            writer.Write(0);
            writer.Write(EffectBanks.Count);
            writer.Write(TextureBanks.Count);
            writer.Write(Materials.Count);
            writer.Write(ParticleMaterial.Size);
            writer.CreateNamedField("ParticleEffectBankOffset");
            writer.CreateNamedField("ParticleTextureBankOffset");
            writer.CreateNamedField("MaterialOffset");

            writer.WriteNamedField("ParticleEffectBankOffset", (uint)writer.Position - BINAHeader.Size);

            foreach (var effectBank in EffectBanks)
                writer.WriteStringFixedLength(effectBank, 0x20);

            writer.WriteNamedField("ParticleTextureBankOffset", (uint)writer.Position - BINAHeader.Size);

            foreach (var textureBank in TextureBanks)
                writer.WriteStringFixedLength(textureBank, 0x20);

            writer.WriteNamedField("MaterialOffset", (uint)writer.Position - BINAHeader.Size);

            foreach (var material in Materials)
            {
                writer.WriteStringFixedLength(material.Name, 0x20);
                writer.WriteStringFixedLength(material.Properties, 0x40);
                writer.Write((uint)material.BlendMode);
            }

            writer.FinishWrite();
        }
    }

    public class ParticleMaterial
    {
        public const int Size = 0x64;

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
        public ParticleBlendMode BlendMode { get; set; }

        public ParticleMaterial() { }

        public ParticleMaterial(string in_name, string in_properties, ParticleBlendMode in_blendMode)
        {
            Name = in_name;
            Properties = in_properties;
            BlendMode = in_blendMode;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ParticleBlendMode
    {
        Additive,
        Negation,
        Opaque = 3
    }
}
