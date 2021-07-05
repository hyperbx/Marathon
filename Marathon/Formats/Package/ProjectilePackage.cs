using System.IO;
using System.Collections.Generic;
using Marathon.IO;
using Newtonsoft.Json;

namespace Marathon.Formats.Package
{
    public class ShotParameter
    {
        public string Name { get; set; }

        public string Model { get; set; }

        /// <summary>
        /// TODO: Unknown - only used by the Egg Buster missile?
        /// </summary>
        public string UnknownString_1 { get; set; }

        /// <summary>
        /// TODO: Unknown - only used by the Egg Buster missile?
        /// </summary>
        public string UnknownString_2 { get; set; }

        public uint UnknownUInt32_1 { get; set; }

        public float UnknownFloat_1 { get; set; }

        public float UnknownFloat_2 { get; set; }

        public uint UnknownUInt32_2 { get; set; }

        public float UnknownFloat_3 { get; set; }

        public float UnknownFloat_4 { get; set; }

        public float UnknownFloat_5 { get; set; }

        public float UnknownFloat_6 { get; set; }

        public float UnknownFloat_7 { get; set; }

        public uint UnknownUInt32_3 { get; set; }

        public float UnknownFloat_8 { get; set; }

        public float UnknownFloat_9 { get; set; }

        public string Explosion { get; set; }

        public string ParticleBank1 { get; set; }

        public string ParticleName1 { get; set; }

        public string SoundBank { get; set; }

        public string SoundName { get; set; }

        public string ParticleBank2 { get; set; }

        public string ParticleName2 { get; set; }

        public string ParticleBank3 { get; set; }

        public string ParticleName3 { get; set; }

        /// <summary>
        /// TODO: Unknown - either empty or misfired stuff.
        /// </summary>
        public string UnknownString_3 { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the ShotParameter.bin format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for projectile properties.</para>
    /// </summary>
    public class ProjectilePackage : FileBase
    {
        public ProjectilePackage() { }

        public ProjectilePackage(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Parameters = JsonDeserialise<List<ShotParameter>>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public const string Extension = ".bin";

        public List<ShotParameter> Parameters = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            // Store first offset after the header.
            long startPosition = reader.BaseStream.Position;

            // Store the offset to the first string entry (repurposed as offset table length).
            uint offsetTableLength = reader.ReadUInt32();

            // Jump back to the first offset so we can iterate through the loop.
            reader.JumpTo(startPosition);

            // Read stream until we've reached the end of the offset table.
            while (reader.BaseStream.Position < offsetTableLength)
            {
                ShotParameter entry = new();

                uint NameOffset            = reader.ReadUInt32();
                uint ModelOffset           = reader.ReadUInt32();
                uint UnknownString_1Offset = reader.ReadUInt32();
                uint UnknownString_2Offset = reader.ReadUInt32();
                entry.UnknownUInt32_1      = reader.ReadUInt32();
                entry.UnknownFloat_1       = reader.ReadSingle();
                entry.UnknownFloat_2       = reader.ReadSingle();
                entry.UnknownUInt32_2      = reader.ReadUInt32();
                entry.UnknownFloat_3       = reader.ReadSingle();
                entry.UnknownFloat_4       = reader.ReadSingle();
                entry.UnknownFloat_5       = reader.ReadSingle();
                entry.UnknownFloat_6       = reader.ReadSingle();
                entry.UnknownFloat_7       = reader.ReadSingle();
                entry.UnknownUInt32_3      = reader.ReadUInt32();
                entry.UnknownFloat_8       = reader.ReadSingle();
                entry.UnknownFloat_9       = reader.ReadSingle();
                uint ExplosionOffset       = reader.ReadUInt32();
                uint ParticleBank1Offset   = reader.ReadUInt32();
                uint ParticleName1Offset   = reader.ReadUInt32();
                uint SoundBankOffset       = reader.ReadUInt32();
                uint SoundOffset           = reader.ReadUInt32();
                uint ParticleBank2Offset   = reader.ReadUInt32();
                uint ParticleName2Offset   = reader.ReadUInt32();
                uint ParticleBank3Offset   = reader.ReadUInt32();
                uint ParticleName3Offset   = reader.ReadUInt32();
                uint UnknownString_3Offset = reader.ReadUInt32();

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // Read all the string values.
                entry.Name            = reader.ReadNullTerminatedString(false, NameOffset, true);
                entry.Model           = reader.ReadNullTerminatedString(false, ModelOffset, true);
                entry.UnknownString_1 = reader.ReadNullTerminatedString(false, UnknownString_1Offset, true);
                entry.UnknownString_2 = reader.ReadNullTerminatedString(false, UnknownString_2Offset, true);
                entry.Explosion       = reader.ReadNullTerminatedString(false, ExplosionOffset, true);
                entry.ParticleBank1   = reader.ReadNullTerminatedString(false, ParticleBank1Offset, true);
                entry.ParticleName1   = reader.ReadNullTerminatedString(false, ParticleName1Offset, true);
                entry.SoundBank       = reader.ReadNullTerminatedString(false, SoundBankOffset, true);
                entry.SoundName       = reader.ReadNullTerminatedString(false, SoundOffset, true);
                entry.ParticleBank2   = reader.ReadNullTerminatedString(false, ParticleBank2Offset, true);
                entry.ParticleName2   = reader.ReadNullTerminatedString(false, ParticleName2Offset, true);
                entry.ParticleBank3   = reader.ReadNullTerminatedString(false, ParticleBank3Offset, true);
                entry.ParticleName3   = reader.ReadNullTerminatedString(false, ParticleName3Offset, true);
                entry.UnknownString_3 = reader.ReadNullTerminatedString(false, UnknownString_3Offset, true);

                // Jump back to the saved position to read the next object.
                reader.JumpTo(position);

                // Save object entry into the Entries list.
                Parameters.Add(entry);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAWriter writer = new(fileStream);

            // Write the objects.
            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.AddString($"entry{i}Name", Parameters[i].Name);
                writer.AddString($"entry{i}Model", Parameters[i].Model);
                writer.AddString($"entry{i}UnknownString_1", Parameters[i].UnknownString_1);
                writer.AddString($"entry{i}UnknownString_2", Parameters[i].UnknownString_2);
                writer.Write(Parameters[i].UnknownUInt32_1);
                writer.Write(Parameters[i].UnknownFloat_1);
                writer.Write(Parameters[i].UnknownFloat_2);
                writer.Write(Parameters[i].UnknownUInt32_2);
                writer.Write(Parameters[i].UnknownFloat_3);
                writer.Write(Parameters[i].UnknownFloat_4);
                writer.Write(Parameters[i].UnknownFloat_5);
                writer.Write(Parameters[i].UnknownFloat_6);
                writer.Write(Parameters[i].UnknownFloat_7);
                writer.Write(Parameters[i].UnknownUInt32_3);
                writer.Write(Parameters[i].UnknownFloat_8);
                writer.Write(Parameters[i].UnknownFloat_9);
                writer.AddString($"entry{i}Explosion", Parameters[i].Explosion);
                writer.AddString($"entry{i}ParticleBank1", Parameters[i].ParticleBank1);
                writer.AddString($"entry{i}Particle1", Parameters[i].ParticleName1);
                writer.AddString($"entry{i}SceneBank", Parameters[i].SoundBank);
                writer.AddString($"entry{i}Sound", Parameters[i].SoundName);
                writer.AddString($"entry{i}ParticleBank2", Parameters[i].ParticleBank2);
                writer.AddString($"entry{i}Particle2", Parameters[i].ParticleName2);
                writer.AddString($"entry{i}ParticleBank2", Parameters[i].ParticleBank3);
                writer.AddString($"entry{i}Particle3", Parameters[i].ParticleName3);
                writer.AddString($"entry{i}UnknownString_3", Parameters[i].UnknownString_3);
            }

            // Write the footer.
            writer.WriteNulls(4);
            writer.FinishWrite();
        }
    }
}