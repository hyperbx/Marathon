using Amicitia.IO;
using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Marathon.IO
{
    public class BinaryObjectReaderEx : BinaryObjectReader
    {
        private Stack<long> _offsetOrigins = [];

        public long OffsetOrigin => _offsetOrigins.Peek();

        public BinaryObjectReaderEx(string in_filePath, Endianness in_endianness, Encoding in_encoding)
            : base(in_filePath, in_endianness, in_encoding)
        {
            Init();
        }

        public BinaryObjectReaderEx(string in_filePath, FileStreamingMode in_fileStreamingMode, Endianness in_endianness, Encoding in_encoding, int in_bufferSize = 1048576)
            : base(in_filePath, in_fileStreamingMode, in_endianness, in_encoding, in_bufferSize)
        {
            Init();
        }

        public BinaryObjectReaderEx(Stream in_stream, StreamOwnership in_streamOwnership, Endianness in_endianness, Encoding in_encoding = null, string in_fileName = null, int in_blockSize = 1048576)
            : base(in_stream, in_streamOwnership, in_endianness, in_encoding, in_fileName, in_blockSize)
        {
            Init();
        }

        public void Init()
        {
            _offsetOrigins.Push(0);
        }

        public bool ReadBoolean<T>() where T : unmanaged
        {
            return (ulong)Convert.ChangeType(Read<T>(), typeof(ulong)) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ReadObjectEx<T>() where T : IBinarySerializableEx, new()
        {
            var obj = new T();
            ReadObjectEx(ref obj);
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadObjectEx(IBinarySerializableEx in_obj)
        {
            ReadObjectEx(ref in_obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadObjectEx<T>(ref T in_rObj) where T : IBinarySerializableEx
        {
            var prefix = Position;
            in_rObj.Read(this);
            var postfix = Position;

            MaybePopulateSourceInfo(in_rObj, prefix, postfix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ReadObjectEx<T, TContext>(TContext in_context) where T : IBinarySerializableEx<TContext>, new()
        {
            var obj = new T();
            ReadObjectEx(ref obj, in_context);
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadObjectEx<T, TContext>(ref T in_rObj, TContext in_context) where T : IBinarySerializableEx<TContext>
        {
            var prefix = Position;
            in_rObj.Read(this, in_context);
            var postfix = Position;

            MaybePopulateSourceInfo(in_rObj, prefix, postfix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MaybePopulateSourceInfo(IBinarySerializableEx in_value, long in_startOffset, long in_endOffset)
        {
            if (PopulateBinarySourceInfo && in_value is IBinarySourceInfo binarySourceInfo)
                binarySourceInfo.BinarySourceInfo = new BinarySourceInfo(FilePath, in_startOffset, in_endOffset, (int)(in_endOffset - in_startOffset), Endianness);
        }

        public long PushOffsetOrigin(long in_offset)
        {
            _offsetOrigins.Push(in_offset);
            return in_offset;
        }

        public long PopOffsetOrigin()
        {
            return _offsetOrigins.Pop();
        }

        public long CalculateOffset(long in_offset, OffsetType in_offsetType = OffsetType.Absolute)
        {
            if (in_offsetType == OffsetType.Relative)
                return in_offset - OffsetOrigin;

            return in_offset + OffsetOrigin;
        }

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
