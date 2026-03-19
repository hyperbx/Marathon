using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ModelJoin : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public string NodeName { get; set; }

        public ModelJoin() { }

        public ModelJoin(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            var nodeNameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(nodeNameOffset),
                () => NodeName = MomentumString.Read(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            var nodeNameOffset = in_writer.Reserve<uint>();

            MomentumString.Write(in_writer, NodeName, nodeNameOffset);
        }

        public uint GetParamCount()
        {
            return 3;
        }
    }
}
