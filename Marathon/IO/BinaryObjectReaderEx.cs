using Amicitia.IO;
using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using System.IO;
using System.Text;

namespace Marathon.IO
{
    public class BinaryObjectReaderEx : BinaryObjectReader
    {
        public BinaryObjectReaderEx(string in_filePath, Endianness in_endianness, Encoding in_encoding)
            : base(in_filePath, in_endianness, in_encoding) { }

        public BinaryObjectReaderEx(string in_filePath, FileStreamingMode in_fileStreamingMode, Endianness in_endianness, Encoding in_encoding, int in_bufferSize = 1048576)
            : base(in_filePath, in_fileStreamingMode, in_endianness, in_encoding, in_bufferSize) { }

        public BinaryObjectReaderEx(Stream in_stream, StreamOwnership in_streamOwnership, Endianness in_endianness, Encoding in_encoding = null, string in_fileName = null, int in_blockSize = 1048576)
            : base(in_stream, in_streamOwnership, in_endianness, in_encoding, in_fileName, in_blockSize) { }

        public virtual void JumpAhead(long in_offset)
        {
            Seek(in_offset, SeekOrigin.Current);
        }

        public virtual void JumpTo(long in_offset)
        {
            Seek(in_offset, SeekOrigin.Begin);
        }

        public virtual void JumpBehind(long in_offset)
        {
            Seek(-in_offset, SeekOrigin.Current);
        }
    }
}
