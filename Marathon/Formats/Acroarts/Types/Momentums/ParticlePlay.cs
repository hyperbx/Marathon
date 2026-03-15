using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ParticlePlay : IMomentumParamSet
    {
        public string ParticleContainer { get; set; }

        public string ParticleName { get; set; }

        public float Speed { get; set; }

        public uint Mode { get; set; }

        public ParticlePlay() { }

        public ParticlePlay(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            var particleContainerOffset = in_reader.Read<uint>();
            var particleNameOffset = in_reader.Read<uint>();

            Speed = in_reader.Read<float>();
            Mode = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_parentChunk.Offset + particleContainerOffset,
                () => ParticleContainer = in_reader.ReadStringFixedLength(0x80));

            in_reader.ReadAtOffset(in_parentChunk.Offset + particleNameOffset,
                () => ParticleName = in_reader.ReadStringFixedLength(0x80));
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            var particleContainerOffset = in_writer.Reserve<uint>();
            var particleNameOffset = in_writer.Reserve<uint>();

            in_writer.Write(Speed);
            in_writer.Write(Mode);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(particleContainerOffset, (uint)(in_writer.Position - in_parentChunk.Offset));
            in_writer.WriteStringFixedLength(ParticleContainer, 0x80);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(particleNameOffset, (uint)(in_writer.Position - in_parentChunk.Offset));
            in_writer.WriteStringFixedLength(ParticleName, 0x80);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
