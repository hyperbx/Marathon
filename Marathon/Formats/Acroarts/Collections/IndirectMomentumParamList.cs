using Marathon.Formats.Acroarts.Types.Momentums;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Collections
{
    public class IndirectMomentumParamList<T> : IndirectList<T> where T : IMomentumParamSet, new()
    {
        public IndirectMomentumParamList() { }

        public IndirectMomentumParamList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public override T ReadImpl(BinaryObjectReaderEx in_reader)
        {
            var paramCount = in_reader.Read<uint>();
            var paramOffset = in_reader.Read<uint>();

            in_reader.JumpTo(in_reader.CalculateOffset(paramOffset));

            return in_reader.ReadObjectEx<T>();
        }

        public override void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object)
        {
            in_writer.Write(in_object.GetParamCount());
            in_writer.WriteOffset((uint)in_writer.CalculateOffset(in_writer.Position + sizeof(uint), OffsetType.Relative));
            in_writer.WriteObjectEx(in_object);
        }
    }
}
