using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.IO.Types
{
    public class RelocationTable<TOffsetType> : List<TOffsetType>
    {
        public long Offset { get; set; }

        public long Length { get; set; }

        public RelocationTable() { }

        public RelocationTable(long in_offset)
        {
            Offset = in_offset;
        }

        public virtual long Read(BinaryObjectReaderEx in_reader, long in_length)
        {
            throw new NotImplementedException();
        }

        public virtual long Write(BinaryObjectWriterEx in_writer)
        {
            throw new NotImplementedException();
        }

        public void AddOffsets<T>(IEnumerable<T> in_collection)
        {
            AddRange(in_collection.Select(x => Convert.ChangeType(x, typeof(TOffsetType))).Cast<TOffsetType>());
        }
    }
}
