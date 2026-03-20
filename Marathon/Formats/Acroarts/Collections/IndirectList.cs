using Marathon.IO;
using Marathon.IO.Extensions;
using System;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Collections
{
    public class IndirectList
    {
        public static (uint Count, uint Offset) ReadInfo(BinaryObjectReaderEx in_reader)
        {
            var count = in_reader.Read<uint>();
            var offset = in_reader.Read<uint>();

            return (count, offset);
        }

        public static (uint Count, uint Offset) PeekInfo(BinaryObjectReaderEx in_reader)
        {
            var pos = in_reader.Position;
            var result = ReadInfo(in_reader);

            in_reader.JumpTo(pos);

            return result;
        }
    }

    public class IndirectList<T> : List<T>
    {
        private long _infoPtrOffset = 0;
        private readonly List<long> _arrayPtrOffsets = [];

        public IndirectList() { }

        public IndirectList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var (count, offset) = IndirectList.ReadInfo(in_reader);

            in_reader.ReadAtOffset(in_reader.CalculateOffset(offset), () =>
            {
                for (uint i = 0; i < count; i++)
                {
                    var dataOffset = in_reader.Read<uint>();

                    in_reader.ReadAtOffset(in_reader.CalculateOffset(dataOffset), () =>
                    {
                        Add(ReadImpl(in_reader));
                    });
                }
            });
        }

        public virtual T ReadImpl(BinaryObjectReaderEx in_reader)
        {
            throw new NotImplementedException();
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            if (Count <= 0)
            {
                in_writer.WriteZero<long>();
                return;
            }

            in_writer.Write(Count);
            _infoPtrOffset = in_writer.Reserve<uint>();
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            if (Count <= 0)
                return;

            in_writer.WriteReserved(_infoPtrOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative));

            for (int i = 0; i < Count; i++)
                _arrayPtrOffsets.Add(in_writer.Reserve<uint>());
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            for (int i = 0; i < Count; i++)
            {
                in_writer.WriteReserved(_arrayPtrOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                WriteDataImpl(in_writer, this[i]);
            }
        }

        public virtual void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object)
        {
            throw new NotImplementedException();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            WriteInfo(in_writer);
            WriteArray(in_writer);
            WriteData(in_writer);
        }
    }
}
