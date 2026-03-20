using Amicitia.IO;
using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.IO.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Marathon.IO
{
    public class BinaryObjectWriterEx : BinaryObjectWriter
    {
        private Stack<long> _offsetOrigins = [];

        public long OffsetOrigin => _offsetOrigins.Peek();

        public Dictionary<string, long> Offsets { get; } = [];

        public BinaryObjectWriterEx(string in_filePath, Endianness in_endianness, Encoding in_encoding = null)
            : base(in_filePath, in_endianness, in_encoding)
        {
            Init();
        }

        public BinaryObjectWriterEx(string in_filePath, FileStreamingMode in_fileStreamingMode, Endianness in_endianness, Encoding in_encoding = null, int in_bufferSize = 1048576)
            : base(in_filePath, in_fileStreamingMode, in_endianness, in_encoding, in_bufferSize)
        {
            Init();
        }

        public BinaryObjectWriterEx(Stream in_stream, StreamOwnership in_streamOwnership, Endianness in_endianness, Encoding in_encoding = null, string in_fileName = null, int in_blockSize = 1048576)
            : base(in_stream, in_streamOwnership, in_endianness, in_encoding, in_fileName, in_blockSize)
        {
            Init();
        }

        public void Init()
        {
            _offsetOrigins.Push(0);
        }

        public void WriteBoolean<T>(bool in_value) where T : unmanaged
        {
            Write((T)Convert.ChangeType(in_value ? 1 : 0, typeof(T)));
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <param name="in_name">The name of the reserved offset.</param>
        /// <param name="in_offset">The offset to reserve.</param>
        /// <param name="in_size">The size of the space to reserve.</param>
        public long Reserve(string in_name, long in_offset, int in_size)
        {
            // Create padding.
            this.WriteZero<byte>(in_size);

            AddOffset(in_name, in_offset);

            return in_offset;
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <param name="in_name">The name of the reserved offset.</param>
        /// <param name="in_size">The size of the space to reserve.</param>
        public long Reserve(string in_name, int in_size = 4)
        {
            return Reserve(in_name, Position, in_size);
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <typeparam name="T">The type to write.</typeparam>
        /// <param name="in_name">The name of the reserved offset.</param>
        /// <param name="in_offset">The offset to reserve.</param>
        public T Reserve<T>(string in_name, long in_offset) where T : unmanaged
        {
            return (T)Convert.ChangeType(Reserve(in_name, in_offset, Marshal.SizeOf<T>()), typeof(T));
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <typeparam name="T">The type to write.</typeparam>
        /// <param name="in_name">The name of the reserved offset.</param>
        public T Reserve<T>(string in_name) where T : unmanaged
        {
            return (T)Convert.ChangeType(Reserve<T>(in_name, Position), typeof(T));
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <param name="in_offset">The offset to reserve.</param>
        /// <param name="in_size">The size of the space to reserve.</param>
        /// <param name="in_isLocal">Determines whether the reserved offset should be added to the relocation table.</param>
        public long Reserve(long in_offset, int in_size, bool in_isLocal = false)
        {
            // Create padding.
            this.WriteZero<byte>(in_size);

            if (!in_isLocal)
                AddOffset(in_offset);

            return in_offset;
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <param name="in_size">The size of the space to reserve.</param>
        /// <param name="in_isLocal">Determines whether the reserved offset should be added to the relocation table.</param>
        public long Reserve(int in_size = 4, bool in_isLocal = false)
        {
            return Reserve(Position, in_size, in_isLocal);
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <typeparam name="T">The type to write.</typeparam>
        /// <param name="in_offset">The offset to reserve.</param>
        /// <param name="in_isLocal">Determines whether the reserved offset should be added to the relocation table.</param>
        public long Reserve<T>(long in_offset, bool in_isLocal = false) where T : unmanaged
        {
            return Reserve(in_offset, Marshal.SizeOf<T>(), in_isLocal);
        }

        /// <summary>
        /// Reserves space at the current position for writing to later using <b>WriteReserved</b>.
        /// </summary>
        /// <typeparam name="T">The type to write.</typeparam>
        /// <param name="in_isLocal">Determines whether the reserved offset should be added to the relocation table.</param>
        public long Reserve<T>(bool in_isLocal = false) where T : unmanaged
        {
            return Reserve<T>(Position, in_isLocal);
        }

        /// <summary>
        /// Writes a value to an offset created by a <b>Reserve</b> method.
        /// </summary>
        /// <typeparam name="T">The type to write.</typeparam>
        /// <param name="in_offset">The offset to write to.</param>
        /// <param name="in_value">The value to write.</param>
        /// <param name="in_removeAfterWrite">Determines whether the reserved offset should be removed from the relocation table.</param>
        public virtual void WriteReserved<T>(long in_offset, T in_value, bool in_removeAfterWrite = false) where T : unmanaged
        {
            this.WriteAtOffset(in_offset, () => Write(in_value));

            if (!in_removeAfterWrite)
                return;

            if (!Offsets.ContainsValue(in_offset))
                return;

            RemoveOffset(in_offset);
        }

        /// <summary>
        /// Writes a value to an offset created by a <b>Reserve</b> method.
        /// </summary>
        /// <typeparam name="T">The type to write.</typeparam>
        /// <param name="in_name">The name of the offset to write to.</param>
        /// <param name="in_value">The value to write.</param>
        /// <param name="in_removeAfterWrite">Determines whether the reserved offset should be removed from the relocation table.</param>
        public virtual void WriteReserved<T>(string in_name, T in_value, bool in_removeAfterWrite = false) where T : unmanaged
        {
            if (!Offsets.TryGetValue(in_name, out var out_offset))
                return;

            this.WriteAtOffset(out_offset, () => Write(in_value));

            if (!in_removeAfterWrite)
                return;

            RemoveOffset(in_name);
        }

        public long AddOffset(string in_name, long in_offset)
        {
            if (Offsets.ContainsKey(in_name))
            {
                Offsets[in_name] = in_offset;
                return in_offset;
            }

            Offsets.Add(in_name, in_offset);

            return in_offset;
        }

        public long AddOffset(long in_offset)
        {
            return AddOffset(Guid.NewGuid().ToString("B"), in_offset);
        }

        public long AddOffset()
        {
            return AddOffset(Position);
        }

        public void RemoveOffset(string in_name)
        {
            Offsets.Remove(in_name);
        }

        public void RemoveOffset(long in_offset)
        {
            var offsets = Offsets.Where(x => x.Value == in_offset);

            for (int i = 0; i < offsets.Count(); i++)
                Offsets.Remove(offsets.ElementAt(i).Key);
        }

        public void RemoveOffset()
        {
            RemoveOffset(Position);
        }

        /// <summary>
        /// Writes a value and stores its offset in the offset list.
        /// </summary>
        /// <typeparam name="T">The type to write.</typeparam>
        /// <param name="in_value">The value to write.</param>
        public long WriteOffset<T>(T in_value) where T : unmanaged
        {
            var offset = Reserve<T>();

            WriteReserved(offset, in_value, false);

            return offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteObjectEx<T>(T in_value) where T : IBinarySerializableEx
        {
            in_value.Write(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteObjectEx<T, TContext>(T in_value, TContext in_context) where T : IBinarySerializableEx<TContext>
        {
            in_value.Write(this, in_context);
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
