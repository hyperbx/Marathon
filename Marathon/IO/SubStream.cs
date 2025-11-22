using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Marathon.IO
{
    public class SubStream : Stream
    {
        private long _position;

        /// <summary>
        /// The stream of which this stream is a substream of.
        /// </summary>
        public Stream BaseStream { get; }

        /// <summary>
        /// The start position of the substream in <see cref="BaseStream"/>.
        /// </summary>
        public long Begin { get; }

        /// <summary>
        /// The end position of the substream in <see cref="BaseStream"/>.
        /// </summary>
        public long End { get; }

        public override bool CanRead => BaseStream.CanRead;

        public override bool CanSeek => BaseStream.CanSeek;

        public override bool CanWrite => BaseStream.CanWrite;

        public override long Length => End - Begin;

        public override long Position
        {
            get => _position;

            set
            {
                BaseStream.Position = Begin + value;
                _position = value;
            }
        }

        public SubStream(Stream in_stream, long in_length)
        {
            BaseStream = in_stream;
            Begin = in_stream.Position;
            End = Begin + in_length;
        }

        public SubStream(Stream in_stream, long in_begin, long in_length)
        {
            BaseStream = in_stream;
            Begin = in_begin;
            End = in_begin + in_length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void EnsurePosition()
        {
            if (BaseStream.Position - Begin != _position)
                Position = _position;
        }

        public override int Read(byte[] in_buffer, int in_offset, int in_count)
        {
            EnsurePosition();

            if (Position >= Length)
                return 0;

            if (Position + in_count >= Length)
                in_count = (int)(Length - Position);

            var bytesRead = BaseStream.Read(in_buffer, in_offset, in_count);

            _position += bytesRead;

            return bytesRead;
        }

        public override void Write(byte[] in_buffer, int in_offset, int in_count)
        {
            EnsurePosition();

            _position += in_count;

            BaseStream.Write(in_buffer, in_offset, in_count);
        }

        public override long Seek(long in_offset, SeekOrigin in_origin)
        {
            switch (in_origin)
            {
                case SeekOrigin.Begin:
                    Position = in_offset;
                    break;

                case SeekOrigin.Current:
                    Position += in_offset;
                    EnsurePosition();
                    break;

                case SeekOrigin.End:
                    Position = Length - in_offset;
                    break;
            }

            return Position;
        }

        public override void SetLength(long in_value)
        {
            throw new NotImplementedException();
        }

        public override void Flush()
        {
            BaseStream.Flush();
        }
    }
}
