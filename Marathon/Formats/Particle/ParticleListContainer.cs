using System.IO;
using System.Collections.Generic;
using Marathon.IO;

namespace Marathon.Formats.Particle
{
    public class ParticleList
    {
        /// <summary>
        /// Name of this particle.
        /// </summary>
        public string ParticleName { get; set; }

        /// <summary>
        /// Name of this particle's data in the PEB referenced in File.
        /// </summary>
        public string EffectName { get; set; }

        /// <summary>
        /// Name of the PEB file, if EffectName is not null, for this particle, otherwise, name of MAB file for this particle.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// <para>0x0 = File uses a MAB.</para>
        /// <para>0x1 = Unknown, only used on kdv_scaffold01, file referenced is nowhere to be found.</para>
        /// <para>0x2 = File uses a PE.</para>
        /// <para>0x10000 = Unknown, can be combined with the other values for something.</para>
        /// </summary>
        public uint Flags { get; set; }

        public override string ToString() => ParticleName;
    }
    public class ParticleListContainer : FileBase
    {
        public ParticleListContainer() { }

        public ParticleListContainer(string file)
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

        public const string Extension = ".plc";

        public class FormatData
        {
            public string Name { get; set; }

            public List<ParticleList> Particles = new();

            public override string ToString() => Name;
        }

        public FormatData Data = new();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new(fileStream);

            // Get Name.
            uint NameOffset = reader.ReadUInt32();
            long position = reader.BaseStream.Position;
            Data.Name = reader.ReadNullTerminatedString(false, NameOffset, true);
            reader.JumpTo(position);

            // Store offsets for the table of entries.
            uint TableOffset     = reader.ReadUInt32(); // Should always be 0x0C
            uint TableEntryCount = reader.ReadUInt32();

            reader.JumpTo(TableOffset, true); // Should already be here but just to be safe.

            // Particle Entries.
            for (int i = 0; i < TableEntryCount; i++)
            {
                ParticleList particle = new();
                uint ParticleNameOffset1 = reader.ReadUInt32();
                uint ParticleNameOffset2 = reader.ReadUInt32();
                uint PENameOffset = reader.ReadUInt32();
                particle.Flags = (uint)reader.ReadUInt32();
                position = reader.BaseStream.Position;

                // Check offsets exist before jumping.
                if (ParticleNameOffset1 != 0)
                    particle.ParticleName = reader.ReadNullTerminatedString(false, ParticleNameOffset1, true);

                if (ParticleNameOffset2 != 0)
                    particle.EffectName = reader.ReadNullTerminatedString(false, ParticleNameOffset2, true);

                if (PENameOffset != 0)
                    particle.File = reader.ReadNullTerminatedString(false, PENameOffset, true);

                // Jump back to the saved position to read the next particle.
                reader.JumpTo(position);

                // Save particle entry into the Particles list.
                Data.Particles.Add(particle);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAWriter writer = new(fileStream);

            // Header
            writer.AddString("Name", Data.Name);
            writer.AddOffset("TableOffset");
            writer.Write(Data.Particles.Count);
            writer.FillInOffset("TableOffset", true);

            // Particle Entries
            for (int i = 0; i < Data.Particles.Count; i++)
            {
                writer.AddString($"particle{i}ParticleName", Data.Particles[i].ParticleName);
                if (Data.Particles[i].EffectName != null)
                    writer.AddString($"particle{i}EffectName", Data.Particles[i].EffectName);
                else
                    writer.WriteNulls(0x4);
                writer.AddString($"particle{i}FileName", Data.Particles[i].File);
                writer.Write(Data.Particles[i].Flags);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }
}
