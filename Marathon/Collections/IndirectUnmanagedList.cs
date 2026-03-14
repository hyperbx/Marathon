using Marathon.IO;

namespace Marathon.Collections
{
    public class IndirectUnmanagedList<T> : IndirectList<T> where T : unmanaged
    {
        public IndirectUnmanagedList() { }

        public IndirectUnmanagedList(BinaryObjectReaderEx in_reader, long in_offset = 0)
        {
            Read(in_reader, in_offset);
        }

        public override T ReadImpl(BinaryObjectReaderEx in_reader, long in_offset = 0)
        {
            return in_reader.Read<T>();
        }

        public override void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object, long in_offset = 0, bool in_keepOffsets = true)
        {
            in_writer.Write(in_object);
        }
    }
}
