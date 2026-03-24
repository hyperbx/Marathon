using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class PlaceFanShaped : IMomentumParamSet
    {
        public AxisType Axis { get; set; }

        public Distance<double> Angle { get; set; } = new(0.0, 360.0);

        public float Radius { get; set; }

        public bool UseLocalSpace { get; set; }

        public PlaceFanShaped() { }

        public PlaceFanShaped(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Axis = in_reader.Read<AxisType>();
            var angleMinOffset = in_reader.Read<uint>();
            var angleMaxOffset = in_reader.Read<uint>();
            Radius = in_reader.Read<float>();
            UseLocalSpace = in_reader.ReadBoolean<uint>();

            in_reader.JumpAhead(8);

            var angleMin = string.Empty;
            var angleMax = string.Empty;

            if (angleMinOffset != 0)
            {
                in_reader.ReadAtOffset(in_reader.CalculateOffset(angleMinOffset),
                    () => angleMin = MomentumString.Read(in_reader));
            }

            if (angleMaxOffset != 0)
            {
                in_reader.ReadAtOffset(in_reader.CalculateOffset(angleMaxOffset),
                    () => angleMax = MomentumString.Read(in_reader));
            }

            if (double.TryParse(angleMin, out var out_angleMin) &&
                double.TryParse(angleMax, out var out_angleMax))
            {
                Angle = new(out_angleMin, out_angleMax);
            }
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Axis);
            var angleMinOffset = in_writer.Reserve<uint>();
            var angleMaxOffset = in_writer.Reserve<uint>();
            in_writer.Write(Radius);
            in_writer.WriteBoolean<uint>(UseLocalSpace);

            MomentumString.Write(in_writer, Angle.Min.ToString("F6"), angleMinOffset);
            MomentumString.Write(in_writer, Angle.Max.ToString("F6"), angleMaxOffset);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
