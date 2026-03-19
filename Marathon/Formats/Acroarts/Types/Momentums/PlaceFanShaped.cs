using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class PlaceFanShaped : IMomentumParamSet
    {
        public AxisType Axis { get; set; }

        public string UnknownField1 { get; set; }

        public string UnknownField2 { get; set; }

        public float Radius { get; set; }

        public uint UnknownField3 { get; set; }

        public PlaceFanShaped() { }

        public PlaceFanShaped(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Axis = in_reader.Read<AxisType>();
            var unkField1Offset = in_reader.Read<uint>();
            var unkField2Offset = in_reader.Read<uint>();
            Radius = in_reader.Read<float>();
            UnknownField3 = in_reader.Read<uint>();

            in_reader.JumpAhead(8);

            in_reader.ReadAtOffset(in_reader.CalculateOffset(unkField1Offset),
                () => UnknownField1 = MomentumString.Read(in_reader));

            in_reader.ReadAtOffset(in_reader.CalculateOffset(unkField2Offset),
                () => UnknownField2 = MomentumString.Read(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Axis);
            var unkField1Offset = in_writer.Reserve<uint>();
            var unkField2Offset = in_writer.Reserve<uint>();
            in_writer.Write(Radius);
            in_writer.Write(UnknownField3);

            MomentumString.Write(in_writer, UnknownField1, unkField1Offset);
            MomentumString.Write(in_writer, UnknownField2, unkField2Offset);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
