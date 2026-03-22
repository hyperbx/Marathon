using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class PlaceLineShaped : IMomentumParamSet
    {
        public Vector3 Start { get; set; }

        public Vector3 End { get; set; }

        public PlaceLineShaped() { }

        public PlaceLineShaped(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Start = in_reader.Read<Vector3>();
            End = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Start);
            in_writer.Write(End);
        }

        public uint GetParamCount()
        {
            return 6;
        }
    }
}
