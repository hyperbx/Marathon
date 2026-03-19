using Marathon.IO;

namespace Marathon.Collections
{
    public class IndirectUnmanagedList<T> : IndirectList<T> where T : unmanaged
    {
        public IndirectUnmanagedList() { }

        public IndirectUnmanagedList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public override T ReadImpl(BinaryObjectReaderEx in_reader)
        {
            return in_reader.Read<T>();
        }

        public override void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object, bool in_keepOffsets = true)
        {
            in_writer.Write(in_object);
        }
    }
}
