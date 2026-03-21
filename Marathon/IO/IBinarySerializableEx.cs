using Amicitia.IO.Binary;

namespace Marathon.IO
{
    public interface IBinarySerializableEx : IBaseBinarySerializable
    {
        void Read(BinaryObjectReaderEx in_reader);

        void Write(BinaryObjectWriterEx in_writer);
    }

    public interface IBinarySerializableEx<TContext> : IBinarySerializableEx, IBaseBinarySerializable
    {
        void IBinarySerializableEx.Read(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader, default);
        }

        void IBinarySerializableEx.Write(BinaryObjectWriterEx in_writer)
        {
            Write(in_writer, default);
        }

        void Read(BinaryObjectReaderEx in_reader, TContext in_context);

        void Write(BinaryObjectWriterEx in_writer, TContext in_context);
    }
}
