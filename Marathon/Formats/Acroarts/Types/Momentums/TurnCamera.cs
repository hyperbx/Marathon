using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TurnCamera : IMomentumParamSet
    {
        public FaceType FaceType { get; set; }

        public float Radius { get; set; }

        public AxisType Axis { get; set; }

        public TurnCamera() { }

        public TurnCamera(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            FaceType = in_reader.Read<FaceType>();
            Radius = in_reader.Read<float>();
            Axis = in_reader.Read<AxisType>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(FaceType);
            in_writer.Write(Radius);
            in_writer.Write(Axis);
        }

        public uint GetParamCount()
        {
            return 3;
        }
    }
}
