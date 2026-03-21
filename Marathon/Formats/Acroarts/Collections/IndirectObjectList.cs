using Marathon.IO;

namespace Marathon.Formats.Acroarts.Collections
{
    public class IndirectObjectList<T> : IndirectList<T> where T : IBinarySerializableEx, new()
    {
        public IndirectObjectList() { }

        public IndirectObjectList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public override T ReadImpl(BinaryObjectReaderEx in_reader)
        {
            return in_reader.ReadObjectEx<T>();
        }

        public override void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object)
        {
            in_writer.WriteObjectEx(in_object);
        }
    }
}
