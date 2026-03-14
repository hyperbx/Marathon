using Amicitia.IO.Binary;
using Marathon.IO;

namespace Marathon.Collections
{
    public class IndirectObjectList<T> : IndirectList<T> where T : IBinarySerializable, new()
    {
        public IndirectObjectList() { }

        public IndirectObjectList(BinaryObjectReaderEx in_reader, long in_offset = 0)
        {
            Read(in_reader, in_offset);
        }

        public override T ReadImpl(BinaryObjectReaderEx in_reader, long in_offset = 0)
        {
            return in_reader.ReadObject<T>();
        }

        public override void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object, long in_offset = 0, bool in_keepOffsets = true)
        {
            in_writer.WriteObject(in_object);
        }
    }
}
