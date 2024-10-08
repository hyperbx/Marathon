﻿namespace Marathon.Formats.Particle
{
    /// <summary>
    /// File base for the *.ptb format.
    /// <para>Used in SONIC THE HEDGEHOG for defining textures for particle effects.</para>
    /// </summary>
    public class ParticleTextureBank : FileBase
    {
        public ParticleTextureBank() { }

        public ParticleTextureBank(string file, bool serialise = false)
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

        public override string Signature { get; } = "BTEP";

        public override string Extension { get; } = ".ptb";

        public class FormatData
        {
            public string Name { get; set; }

            public List<ParticleTexture> ParticleTextures { get; set; } = [];

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

            // Particle Texture Entries.
            for (int i = 0; i < EntryCount; i++)
            {
                ParticleTexture particle = new()
                {
                    Name     = reader.ReadNullPaddedString(0x20),
                    Path = reader.ReadNullPaddedString(0x80),
                    Width    = reader.ReadUInt32(),
                    Height   = reader.ReadUInt32()
                };

                // Save particle texture entry into the ParticleTextures list.
                Data.ParticleTextures.Add(particle);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            // Header
            writer.WriteSignature(Signature);
            writer.WriteNulls(0x8);
            writer.Write(Data.ParticleTextures.Count);
            writer.WriteNullPaddedString(Data.Name, 0x20);
            writer.AddOffset("OffsetTable");

            writer.FillOffset("OffsetTable", true);

            // Particle Texture Entries
            for (int i = 0; i < Data.ParticleTextures.Count; i++)
            {
                writer.WriteNullPaddedString(Data.ParticleTextures[i].Name, 0x20);
                writer.WriteNullPaddedString(Data.ParticleTextures[i].Path, 0x80);
                writer.Write(Data.ParticleTextures[i].Width);
                writer.Write(Data.ParticleTextures[i].Height);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }

    public class ParticleTexture
    {
        /// <summary>
        /// Name of this particle texture.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path to the texture used by this particle.
        /// </summary>
        public string Path { get; set; }

        public uint Width { get; set; }

        public uint Height { get; set; }

        public ParticleTexture() { }

        public ParticleTexture(string in_name, string in_path, uint in_width, uint in_height)
        {
            Name = in_name;
            Path = in_path;
            Width = in_width;
            Height = in_height;
        }

        public override string ToString() => Name;
    }
}
