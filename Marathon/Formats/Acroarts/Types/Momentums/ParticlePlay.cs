using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ParticlePlay : IMomentumParamSet
    {
        public string ParticleContainer { get; set; }

        public string ParticleName { get; set; }

        public float Speed { get; set; }

        public uint Mode { get; set; }

        public ParticlePlay() { }

        public ParticlePlay(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var particleContainerOffset = in_reader.Read<uint>();
            var particleNameOffset = in_reader.Read<uint>();

            Speed = in_reader.Read<float>();
            Mode = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(particleContainerOffset),
                () => ParticleContainer = MomentumString.Read(in_reader));

            in_reader.ReadAtOffset(in_reader.CalculateOffset(particleNameOffset),
                () => ParticleName = MomentumString.Read(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var particleContainerOffset = in_writer.Reserve<uint>();
            var particleNameOffset = in_writer.Reserve<uint>();

            in_writer.Write(Speed);
            in_writer.Write(Mode);

            MomentumString.Write(in_writer, ParticleContainer, particleContainerOffset);
            MomentumString.Write(in_writer, ParticleName, particleNameOffset);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
