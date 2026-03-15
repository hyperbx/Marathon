using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class PlaceFanShaped : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public string UnknownField2 { get; set; }

        public string UnknownField3 { get; set; }

        public float UnknownField4 { get; set; }

        public uint UnknownField5 { get; set; }

        public PlaceFanShaped() { }

        public PlaceFanShaped(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            UnknownField1 = in_reader.Read<uint>();
            var unkField2Offset = in_reader.Read<uint>();
            var unkField3Offset = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<float>();
            UnknownField5 = in_reader.Read<uint>();

            in_reader.JumpAhead(8);

            in_reader.ReadAtOffset(in_parentChunk.Offset + unkField2Offset,
                () => UnknownField2 = in_reader.ReadStringFixedLength(0x80));

            in_reader.ReadAtOffset(in_parentChunk.Offset + unkField3Offset,
                () => UnknownField3 = in_reader.ReadStringFixedLength(0x80));
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(UnknownField1);
            var unkField2Offset = in_writer.Reserve<uint>();
            var unkField3Offset = in_writer.Reserve<uint>();
            in_writer.Write(UnknownField4);
            in_writer.Write(UnknownField5);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(unkField2Offset, (uint)(in_writer.Position - in_parentChunk.Offset));
            in_writer.WriteStringFixedLength(UnknownField2, 0x80);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(unkField3Offset, (uint)(in_writer.Position - in_parentChunk.Offset));
            in_writer.WriteStringFixedLength(UnknownField3, 0x80);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
