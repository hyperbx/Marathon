using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Particle
{
    public class ParticleTexture
    {
        /// <summary>
        /// Name of this particle texture.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Filepath to the texture used by this particle.
        /// </summary>
        public string Filepath { get; set; }

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

    public class ParticleTextureBank : FileBase
    {
        public ParticleTextureBank() { }

        public ParticleTextureBank(string file)
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

        public const string Signature = "BTEP",
                            Extension = ".ptb";

        public class FormatData
        {
            public string Name { get; set; }

            public List<ParticleTexture> ParticleTextures = new();

            public override string ToString() => Name;
        }

        public FormatData Data = new();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new(fileStream);

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
                    Filepath = new string(reader.ReadChars(0x80)).Trim('\0'),
                    UnknownUInt32_1 = reader.ReadUInt32(),
                    UnknownUInt32_2 = reader.ReadUInt32()
                };

                // Save particle texture entry into the ParticleTextures list.
                Data.ParticleTextures.Add(particle);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAWriter writer = new(fileStream);

            // Header
            writer.WriteSignature(Signature);
            writer.WriteNulls(0x8);
            writer.Write(Data.ParticleTextures.Count);
            writer.WriteNullPaddedString(Data.Name, 0x20);
            writer.AddOffset("OffsetTable");

            writer.FillInOffset("OffsetTable", true);

            // Particle Texture Entries
            for (int i = 0; i < Data.ParticleTextures.Count; i++)
            {
                writer.WriteNullPaddedString(Data.ParticleTextures[i].Name, 0x20);
                writer.WriteNullPaddedString(Data.ParticleTextures[i].Filepath, 0x80);
                writer.Write(Data.ParticleTextures[i].UnknownUInt32_1);
                writer.Write(Data.ParticleTextures[i].UnknownUInt32_2);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }
}
