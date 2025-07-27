using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;

// Format names:        Particle Container
// Format references:   Sonicteam::Particles::ParticleContainer
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Particle
{
    /// <summary>
    /// Support for *.plc files; used for defining particle effects.
    /// </summary>
    public class ParticleContainer : FileBase
    {
        private const string _extension = ".plc"; // "ParticLe Container"

        public ParticleContainer() { }

        public ParticleContainer(string in_path) : base(in_path) { }

        /// <summary>
        /// The name of this particle container.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The particles in this container.
        /// </summary>
        public List<Particle> Particles { get; set; } = [];

        public Particle this[string in_name]
        {
            get => Particles.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var nameOffset = reader.Read<uint>();

            reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                () => Name = reader.ReadStringNullTerminated());

            var particleTableCount = reader.Read<uint>();
            var particleCount = reader.Read<uint>();

            for (int i = 0; i < particleCount; i++)
            {
                var particle = new Particle();

                var particleNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameOffset,
                    () => particle.Name = reader.ReadStringNullTerminated());

                var effectNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + effectNameOffset,
                    () => particle.EffectBankName = reader.ReadStringNullTerminated());

                var effectBankOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + effectBankOffset,
                    () => particle.Resource = reader.ReadStringNullTerminated());

                particle.Flags = reader.Read<uint>();

                Particles.Add(particle);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteStringOffset(Name);
            writer.Reserve<uint>("ParticleTableOffset");
            writer.Write(Particles.Count);
            writer.WriteReserved("ParticleTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Particles.Count; i++)
            {
                writer.WriteStringOffset(Particles[i].Name);

                if (Particles[i].EffectBankName == null)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.WriteStringOffset(Particles[i].EffectBankName);
                }

                writer.WriteStringOffset(Particles[i].Resource);
                writer.Write(Particles[i].Flags);
            }

            writer.FinishWrite();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Particle
    {
        /// <summary>
        /// The name of this particle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the effect bank entry this particle uses.
        /// <para>If null, this particle uses a *.mab file.</para>
        /// </summary>
        public string EffectBankName { get; set; }

        /// <summary>
        /// The location of the resource this particle uses.
        /// <para>If <see cref="EffectBankName"/> is null, this should be the location of a *.mab file.</para>
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// TODO: unknown, bitfield?
        /// <para>0x00000000 - uses a *.mab file.</para>
        /// <para>0x00000001 - only used on kdv_scaffold01, the resource referenced does not exist.</para>
        /// <para>0x00000002 - uses a particle effect bank.</para>
        /// <para>0x00010000 - unknown, can be combined with the other values for something.</para>
        /// </summary>
        public uint Flags { get; set; }

        public Particle() { }

        public Particle(string in_particleName, string in_effectName, string in_file, uint in_flags)
        {
            Name = in_particleName;
            EffectBankName = in_effectName;
            Resource = in_file;
            Flags = in_flags;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
