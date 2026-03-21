using Marathon.IO;
using System.Runtime.InteropServices;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class SplineKeyFrame<T> : IMomentumParamSet where T : unmanaged
    {
        public float Frame { get; set; }

        public T Data { get; set; }

        public float Tension { get; set; }

        public float Continuity { get; set; }

        public float Bias { get; set; }

        public float EaseTo { get; set; }

        public float EaseFrom { get; set; }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Frame = in_reader.Read<float>();
            Data = in_reader.Read<T>();
            Tension = in_reader.Read<float>();
            Continuity = in_reader.Read<float>();
            Bias = in_reader.Read<float>();
            EaseTo = in_reader.Read<float>();
            EaseFrom = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Frame);
            in_writer.Write(Data);
            in_writer.Write(Tension);
            in_writer.Write(Continuity);
            in_writer.Write(Bias);
            in_writer.Write(EaseTo);
            in_writer.Write(EaseFrom);
        }

        public uint GetParamCount()
        {
            return (uint)(6 + (Marshal.SizeOf<T>() / 4));
        }
    }
}
