using Amicitia.IO.Binary;
using Marathon.IO;

namespace Marathon.Collections
{
    public class IndirectObjectList<T> : IndirectList<T> where T : IBinarySerializable, new()
    {
        public IndirectObjectList() { }

        public IndirectObjectList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public override T ReadImpl(BinaryObjectReaderEx in_reader)
        {
            return in_reader.ReadObject<T>();
        }

        public override void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object, bool in_keepOffsets = true)
        {
            in_writer.WriteObject(in_object);
        }
    }
}
