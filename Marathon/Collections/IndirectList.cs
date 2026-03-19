using Marathon.IO;
using Marathon.IO.Extensions;
using System;
using System.Collections.Generic;

namespace Marathon.Collections
{
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
            var count = in_reader.Read<uint>();
            var arrayOffset = in_reader.Read<uint>();
            var position = in_reader.Position;

            in_reader.ReadAtOffset(in_reader.CalculateOffset(arrayOffset), () =>
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

        public void WriteInfo(BinaryObjectWriterEx in_writer, bool in_keepOffsets = true)
        {
            if (Count <= 0)
            {
                in_writer.WriteZero<long>();
                return;
            }

            in_writer.Write(Count);
            _infoPtrOffset = in_writer.Reserve<uint>(!in_keepOffsets);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, bool in_keepOffsets = true)
        {
            if (Count <= 0)
                return;

            in_writer.WriteReserved(_infoPtrOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), !in_keepOffsets);

            for (int i = 0; i < Count; i++)
                _arrayPtrOffsets.Add(in_writer.Reserve<uint>(!in_keepOffsets));
        }

        public void WriteData(BinaryObjectWriterEx in_writer, bool in_keepOffsets = true)
        {
            for (int i = 0; i < Count; i++)
            {
                in_writer.WriteReserved(_arrayPtrOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), !in_keepOffsets);
                WriteDataImpl(in_writer, this[i], in_keepOffsets);
            }
        }

        public virtual void WriteDataImpl(BinaryObjectWriterEx in_writer, T in_object, bool in_keepOffsets = true)
        {
            throw new NotImplementedException();
        }

        public void Write(BinaryObjectWriterEx in_writer, bool in_keepOffsets = true)
        {
            WriteInfo(in_writer, in_keepOffsets);
            WriteArray(in_writer, in_keepOffsets);
            WriteData(in_writer, in_keepOffsets);
        }
    }
}
