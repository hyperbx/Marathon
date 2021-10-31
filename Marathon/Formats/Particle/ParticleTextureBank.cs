using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Particle
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

            public List<ParticleTexture> ParticleTextures { get; set; } = new();

            public override string ToString() => Name;
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);
            reader.JumpAhead(0x8); // Always Null.
            uint EntryCount = reader.ReadUInt32();
            Data.Name = new string(reader.ReadChars(0x20)).Trim('\0');
            uint OffsetTable = reader.ReadUInt32();

            reader.JumpTo(OffsetTable, true); // Should already be here but just to be safe.

            // Particle Texture Entries.
            for (int i = 0; i < EntryCount; i++)
            {
                ParticleTexture particle = new()
                {
                    Name = new string(reader.ReadChars(0x20)).Trim('\0'),
                    FilePath = new string(reader.ReadChars(0x80)).Trim('\0'),
                    UnknownUInt32_1 = reader.ReadUInt32(),
                    UnknownUInt32_2 = reader.ReadUInt32()
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
                writer.WriteNullPaddedString(Data.ParticleTextures[i].FilePath, 0x80);
                writer.Write(Data.ParticleTextures[i].UnknownUInt32_1);
                writer.Write(Data.ParticleTextures[i].UnknownUInt32_2);
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
        /// Filepath to the texture used by this particle.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Unknown, always the same as UnknownUInt32_2.
        /// </summary>
        public uint UnknownUInt32_1 { get; set; }

        /// <summary>
        /// Unknown, always the same as UnknownUInt32_1.
        /// </summary>
        public uint UnknownUInt32_2 { get; set; }

        public override string ToString() => Name;
    }
}
