using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Particle
{
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
        public BlendMode Blending { get; set; }

        /// <summary>
        /// The size of this struct.
        /// <para>0 = disables all particles</para>
        /// <para>64 = full struct size</para>
        /// <para>??? = game crash</para>
        /// </summary>
        public const uint Size = 0x64;

        public override string ToString() => Name;
    }

    public class ParticleGenerationSystem : FileBase
    {
        public ParticleGenerationSystem() { }

        public ParticleGenerationSystem(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Data = JsonDeserialise<FormatData>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public const string Signature = "SGEP",
                            Extension = ".pgs";

        public class FormatData
        {
            public List<string> Effects = new();

            public List<string> Textures = new();

            public List<ParticleMaterial> Materials = new();
        }

        public FormatData Data = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(0x04, Signature);

            reader.JumpAhead(0x04);
            uint particleEffectBankCount   = reader.ReadUInt32();
            uint particleTextureBankCount  = reader.ReadUInt32();
            uint materialCount             = reader.ReadUInt32();
            uint materialSize              = reader.ReadUInt32(); // Sonic Team moment.
            uint particleEffectBankOffset  = reader.ReadUInt32();
            uint particleTextureBankOffset = reader.ReadUInt32();
            uint materialOffset            = reader.ReadUInt32();

            reader.JumpTo(particleEffectBankOffset, true);
            for (int i = 0; i < particleEffectBankCount; i++)
                Data.Effects.Add(new string(reader.ReadChars(0x20)).Trim('\0'));

            reader.JumpTo(particleTextureBankOffset, true);
            for (int i = 0; i < particleTextureBankCount; i++)
                Data.Textures.Add(new string(reader.ReadChars(0x20)).Trim('\0'));

            reader.JumpTo(materialOffset, true);
            for (int i = 0; i < materialCount; i++)
            {
                ParticleMaterial material = new()
                {
                    Name = new string(reader.ReadChars(0x20)).Trim('\0'),
                    Properties = new string(reader.ReadChars(0x40)).Trim('\0'),
                    Blending = (BlendMode)reader.ReadUInt32()
                };

                Data.Materials.Add(material);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);

            writer.WriteNulls(0x04);
            writer.Write(Data.Effects.Count);
            writer.Write(Data.Textures.Count);
            writer.Write(Data.Materials.Count);
            writer.Write(ParticleMaterial.Size);
            writer.AddOffset("particleEffectBankOffset");
            writer.AddOffset("particleTextureBankOffset");
            writer.AddOffset("materialOffset");

            writer.FillOffset("particleEffectBankOffset", true);

            foreach (string effect in Data.Effects)
                writer.WriteNullPaddedString(effect, 0x20);

            writer.FillOffset("particleTextureBankOffset", true);

            foreach (string texture in Data.Textures)
                writer.WriteNullPaddedString(texture, 0x20);

            writer.FillOffset("materialOffset", true);

            foreach (ParticleMaterial material in Data.Materials)
            {
                writer.WriteNullPaddedString(material.Name, 0x20);
                writer.WriteNullPaddedString(material.Properties, 0x40);
                writer.Write((uint)material.Blending);
            }

            writer.FinishWrite();
        }
    }
}
