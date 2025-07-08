using Amicitia.IO;
using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Marathon.IO
{
    public class BinaryObjectWriterEx : BinaryObjectWriter
    {
        public Dictionary<string, long> Fields { get; } = [];

        public BinaryObjectWriterEx(string in_filePath, Endianness in_endianness, Encoding in_encoding = null)
            : base(in_filePath, in_endianness, in_encoding) { }

        public BinaryObjectWriterEx(string in_filePath, FileStreamingMode in_fileStreamingMode, Endianness in_endianness, Encoding in_encoding = null, int in_bufferSize = 1048576)
            : base(in_filePath, in_fileStreamingMode, in_endianness, in_encoding, in_bufferSize) { }

        public BinaryObjectWriterEx(Stream in_stream, StreamOwnership in_streamOwnership, Endianness in_endianness, Encoding in_encoding = null, string in_fileName = null, int in_blockSize = 1048576)
            : base(in_stream, in_streamOwnership, in_endianness, in_encoding, in_fileName, in_blockSize) { }

        public void CreateNamedField(string in_name, long in_offset, int in_size)
        {
            // Create padding.
            this.WriteNullBytes(in_size);

            if (Fields.ContainsKey(in_name))
            {
                Fields[in_name] = in_offset;
                return;
            }

            Fields.Add(in_name, in_offset);
        }

        public void CreateNamedField(string in_name, int in_size = 4)
        {
            CreateNamedField(in_name, Position, in_size);
        }

        public unsafe void CreateNamedField<T>(string in_name, long in_offset) where T : unmanaged
        {
            CreateNamedField(in_name, in_offset, sizeof(T));
        }

        public unsafe void CreateNamedField<T>(string in_name) where T : unmanaged
        {
            CreateNamedField<T>(in_name, Position);
        }

        public virtual void WriteNamedField<T>(string in_name, T in_value, bool in_removeField = true) where T : unmanaged
        {
            var pos = Position;

            if (!JumpToNamedField(in_name, in_removeField))
                return;

            Write(in_value);
            JumpTo(pos);
        }

        public bool JumpToNamedField(string in_name, bool in_removeField = true)
        {
            if (!Fields.TryGetValue(in_name, out long out_fieldOffset))
                return false;

            JumpTo(out_fieldOffset);

            if (in_removeField)
                Fields.Remove(in_name);

            return true;
        }

        public void JumpAhead(long in_offset)
        {
            Seek(in_offset, SeekOrigin.Current);
        }

        public void JumpTo(long in_offset)
        {
            Seek(in_offset, SeekOrigin.Begin);
        }

        public void JumpBehind(long in_offset)
        {
            Seek(-in_offset, SeekOrigin.Current);
        }
    }
}
