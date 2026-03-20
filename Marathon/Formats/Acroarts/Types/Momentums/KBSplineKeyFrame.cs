using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class KBSplineKeyFrame<T> : SplineKeyFrame<T> where T : unmanaged
    {
        public float Tension { get; set; }

        public float Continuity { get; set; }

        public float Bias { get; set; }

        public float EaseTo { get; set; }

        public float EaseFrom { get; set; }

        public override void Read(BinaryObjectReaderEx in_reader)
        {
            base.Read(in_reader);

            Tension = in_reader.Read<float>();
            Continuity = in_reader.Read<float>();
            Bias = in_reader.Read<float>();
            EaseTo = in_reader.Read<float>();
            EaseFrom = in_reader.Read<float>();
        }

        public override void Write(BinaryObjectWriterEx in_writer)
        {
            base.Write(in_writer);

            in_writer.Write(Tension);
            in_writer.Write(Continuity);
            in_writer.Write(Bias);
            in_writer.Write(EaseTo);
            in_writer.Write(EaseFrom);
        }

        public override uint GetParamCount()
        {
            return base.GetParamCount() + 5;
        }
    }
}
