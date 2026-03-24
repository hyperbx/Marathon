using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateRandomSin : IMomentumParamSet
    {
        public Distance<float> Frequency { get; set; }

        public Distance<float> HighAmplitudeX { get; set; }

        public Distance<float> LowAmplitudeX { get; set; }

        public Distance<float> HighAmplitudeY { get; set; }

        public Distance<float> LowAmplitudeY { get; set; }

        public Distance<float> HighAmplitudeZ { get; set; }

        public Distance<float> LowAmplitudeZ { get; set; }

        public Distance<float> StartAngle { get; set; }

        public uint UnknownField { get; set; }

        public GTCounter GTCounter { get; set; }

        public TranslateVectorType VectorType { get; set; }

        public int Parameter { get; set; }

        public TranslateRandomSin() { }

        public TranslateRandomSin(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Frequency = in_reader.Read<Distance<float>>();
            HighAmplitudeX = in_reader.Read<Distance<float>>();
            LowAmplitudeX = in_reader.Read<Distance<float>>();
            HighAmplitudeY = in_reader.Read<Distance<float>>();
            LowAmplitudeY = in_reader.Read<Distance<float>>();
            HighAmplitudeZ = in_reader.Read<Distance<float>>();
            LowAmplitudeZ = in_reader.Read<Distance<float>>();
            StartAngle = in_reader.Read<Distance<float>>();
            UnknownField = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            VectorType = in_reader.Read<TranslateVectorType>();
            Parameter = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Frequency);
            in_writer.Write(HighAmplitudeX);
            in_writer.Write(LowAmplitudeX);
            in_writer.Write(HighAmplitudeY);
            in_writer.Write(LowAmplitudeY);
            in_writer.Write(HighAmplitudeZ);
            in_writer.Write(LowAmplitudeZ);
            in_writer.Write(StartAngle);
            in_writer.Write(UnknownField);
            in_writer.Write(GTCounter);
            in_writer.Write(VectorType);
            in_writer.Write(Parameter);
        }

        public uint GetParamCount()
        {
            return 20;
        }
    }
}
