using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class AnonymousMomentumParamSet : List<AnonymousMomentumParam>, IMomentumParamSet
    {
        public AnonymousMomentumParamSet() { }

        public AnonymousMomentumParamSet(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            var paramCount = in_reader.Read<uint>();
            var paramOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_parentChunk.Offset + paramOffset, () =>
            {
                for (uint i = 0; i < paramCount; i++)
                    Add(new AnonymousMomentumParam(in_reader));
            });
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            in_writer.Write(Count);
            in_writer.WriteOffset((uint)(in_writer.Position - in_parentChunk.Offset) + sizeof(uint));

            for (int i = 0; i < Count; i++)
                this[i].Write(in_writer);
        }

        public uint GetParamCount()
        {
            return (uint)Count;
        }
    }
}
