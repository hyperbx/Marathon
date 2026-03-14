using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class PlaceFanShaped : IMomentumParamSet
    {
        public string UnknownField1 { get; set; }

        public string UnknownField2 { get; set; }

        public PlaceFanShaped() { }

        public PlaceFanShaped(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            // TODO
            var unkField1 = in_reader.Read<uint>();
            var unkStringOffset1 = in_reader.Read<uint>();
            var unkStringOffset2 = in_reader.Read<uint>();
            var unkField2 = in_reader.Read<float>();
            var unkField3 = in_reader.Read<uint>();

            in_reader.JumpAhead(8); // Reserved.

            in_reader.ReadAtOffset(in_parentChunk.Offset + unkStringOffset1,
                () => UnknownField1 = in_reader.ReadStringFixedLength(0x80));

            in_reader.ReadAtOffset(in_parentChunk.Offset + unkStringOffset2,
                () => UnknownField2 = in_reader.ReadStringFixedLength(0x80));
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            // TODO
            in_writer.Write(1);
            var unkStringOffset1 = in_writer.Reserve<uint>();
            var unkStringOffset2 = in_writer.Reserve<uint>();
            in_writer.Write(10.0f);
            in_writer.Write(1);

            in_writer.JumpAhead(8); // Reserved.

            in_writer.WriteReserved(unkStringOffset1, (uint)(in_writer.Position - in_parentChunk.Offset));
            in_writer.WriteStringFixedLength(UnknownField1, 0x80);

            in_writer.JumpAhead(8); // Reserved.

            in_writer.WriteReserved(unkStringOffset2, (uint)(in_writer.Position - in_parentChunk.Offset));
            in_writer.WriteStringFixedLength(UnknownField2, 0x80);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
