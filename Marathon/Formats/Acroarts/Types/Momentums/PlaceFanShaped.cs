using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

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

        public PlaceFanShaped(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Axis = in_reader.Read<AxisType>();
            var unkField1Offset = in_reader.Read<uint>();
            var unkField2Offset = in_reader.Read<uint>();
            Radius = in_reader.Read<float>();
            UnknownField3 = in_reader.Read<uint>();

            in_reader.JumpAhead(8);

            in_reader.ReadAtOffset(in_parentChunk.Offset + unkField1Offset,
                () => UnknownField1 = in_reader.ReadStringFixedLength(0x80));

            in_reader.ReadAtOffset(in_parentChunk.Offset + unkField2Offset,
                () => UnknownField2 = in_reader.ReadStringFixedLength(0x80));
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Axis);
            var unkField1Offset = in_writer.Reserve<uint>();
            var unkField2Offset = in_writer.Reserve<uint>();
            in_writer.Write(Radius);
            in_writer.Write(UnknownField3);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(unkField1Offset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(UnknownField1, 0x80);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(unkField2Offset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(UnknownField2, 0x80);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
