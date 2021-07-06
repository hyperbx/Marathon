using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Package
{
    public class Explosion
    {
        /// <summary>
        /// The name of the explosion.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TODO: Unknown - setting this to 1 allowed a BombBox explosion to take enemies out, 0 & 2 didn't.
        /// </summary>
        public uint UnknownUInt32_1 { get; set; }

        /// <summary>
        /// The radius that this explosion affects.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// TODO: Unknown - nearly always the same as Radius, barring a few exceptions.
        /// </summary>
        public float UnknownFloat_1 { get; set; }

        /// <summary>
        /// TODO: Unknown.
        /// </summary>
        public float UnknownFloat_2 { get; set; }

        /// <summary>
        /// TODO: Unknown.
        /// </summary>
        public float UnknownFloat_3 { get; set; }

        /// <summary>
        /// TODO: Unknown.
        /// </summary>
        public float UnknownFloat_4 { get; set; }

        /// <summary>
        /// TODO: not too sure, but increasing this value seemed to affect something to do with how the explosion affects other physics objects?
        /// </summary>
        public float Force { get; set; }

        /// <summary>
        /// How much damage this explosion causes.
        /// </summary>
        public uint Damage { get; set; }

        /// <summary>
        /// TODO: how things should react to this explosion? Has a lot of different values, 46 made every explosion stun enemies like a FlashBox and be unable to damage the player.
        /// </summary>
        public uint Behaviour { get; set; }

        /// <summary>
        /// The internal path to the particle file used for the explosion.
        /// </summary>
        public string ParticleFile { get; set; }

        /// <summary>
        /// The name of the particle in the particle file to use for the explosion.
        /// </summary>
        public string ParticleName { get; set; }

        /// <summary>
        /// The internal path to the <see cref="Audio.SoundBank"/> (*.sbk) used for the explosion.
        /// </summary>
        public string SoundBank { get; set; }

        /// <summary>
        /// The name of the sound in the <see cref="Audio.SoundBank"/> to use for the explosion.
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        /// The internal path to the light animation (*.xni) used for the explosion.
        /// </summary>
        public string LightName { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// <para>File base for the Explosion.bin format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for explosion properties.</para>
    /// </summary>
    public class ExplosionPackage : FileBase
    {
        public ExplosionPackage() { }

        public ExplosionPackage(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Explosions = JsonDeserialise<List<Explosion>>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public List<Explosion> Explosions = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            // Store first offset after the header.
            long startPosition = reader.BaseStream.Position;

            // Store the offset to the first string entry (repurposed as offset table length).
            uint offsetTableLength = reader.ReadUInt32();

            // Jump back to the first offset so we can iterate through the loop.
            reader.JumpTo(startPosition);

            while (reader.BaseStream.Position < offsetTableLength)
            {
                Explosion explosion = new();

                uint nameOffset           = reader.ReadUInt32();
                explosion.UnknownUInt32_1 = reader.ReadUInt32();
                explosion.Radius          = reader.ReadSingle();
                explosion.UnknownFloat_1  = reader.ReadSingle();
                explosion.UnknownFloat_2  = reader.ReadSingle();
                explosion.UnknownFloat_3  = reader.ReadSingle();
                explosion.UnknownFloat_4  = reader.ReadSingle();
                explosion.Force           = reader.ReadSingle();
                explosion.Damage          = reader.ReadUInt32();
                explosion.Behaviour       = reader.ReadUInt32();
                uint particleFileOffset   = reader.ReadUInt32();
                uint particleNameOffset   = reader.ReadUInt32();
                uint SceneBankOffset      = reader.ReadUInt32();
                uint soundNameOffset      = reader.ReadUInt32();
                uint lightNameOffset      = reader.ReadUInt32();

                reader.JumpAhead(12); //Padding? All nulls in official file. Might be worth experimenting with to see if anything changes.
                long position = reader.BaseStream.Position;

                // Read all the string values.
                reader.JumpTo(nameOffset, true);
                explosion.Name         = reader.ReadNullTerminatedString(false, nameOffset, true);
                explosion.ParticleFile = reader.ReadNullTerminatedString(false, particleFileOffset, true);
                explosion.ParticleName = reader.ReadNullTerminatedString(false, particleNameOffset, true);
                explosion.SoundBank    = reader.ReadNullTerminatedString(false, SceneBankOffset, true);
                explosion.SoundName    = reader.ReadNullTerminatedString(false, soundNameOffset, true);
                explosion.LightName    = reader.ReadNullTerminatedString(false, lightNameOffset, true);

                reader.JumpTo(position);

                Explosions.Add(explosion);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAWriter writer = new(fileStream);

            // Write the explosion entries.
            for (int i = 0; i < Explosions.Count; i++)
            {
                writer.AddString($"entry{i}Name", Explosions[i].Name);
                writer.Write(Explosions[i].UnknownUInt32_1);
                writer.Write(Explosions[i].Radius);
                writer.Write(Explosions[i].UnknownFloat_1);
                writer.Write(Explosions[i].UnknownFloat_2);
                writer.Write(Explosions[i].UnknownFloat_3);
                writer.Write(Explosions[i].UnknownFloat_4);
                writer.Write(Explosions[i].Force);
                writer.Write(Explosions[i].Damage);
                writer.Write(Explosions[i].Behaviour);
                writer.AddString($"entry{i}ParticleFile", Explosions[i].ParticleFile);
                writer.AddString($"entry{i}ParticleName", Explosions[i].ParticleName);
                writer.AddString($"entry{i}SceneBank", Explosions[i].SoundBank);
                writer.AddString($"entry{i}SoundName", Explosions[i].SoundName);
                writer.AddString($"entry{i}LightName", Explosions[i].LightName);
                writer.WriteNulls(12);
            }

            // Write the footer.
            writer.WriteNulls(4);
            writer.FinishWrite();
        }
    }
}
