// ExtendedBinary.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2018 Radfordhound
 * Copyright (c) 2020 HyperBE32
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Marathon.IO.Formats;

namespace Marathon.IO
{
    public class ExtendedBinaryReader : BinaryReader
    {
        public uint Offset = 0;
        public bool IsBigEndian = false;

        protected Stream _Stream;
        protected byte[] _Buffer;

        protected const string _EndOfStream  = "The end of the stream has been reached!",
                               _StreamClosed = "The stream has been closed!";

        protected const int _MinimumBufferSize = 16;
        protected const char _Null = '\0';

        public ExtendedBinaryReader(Stream input, bool isBigEndian = false) : this(input, Encoding.ASCII, isBigEndian) { }

        public ExtendedBinaryReader(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, false)
        {
            _Stream = input;
            IsBigEndian = isBigEndian;

            int bufferSize = encoding.GetMaxByteCount(1);

            if (bufferSize < _MinimumBufferSize)
                bufferSize = _MinimumBufferSize;

            _Buffer = new byte[bufferSize];
        }

        /// <summary>
        /// Jumps to a position in the stream.
        /// </summary>
        /// <param name="position">Position to jump to.</param>
        /// <param name="absolute">Jumps to the specified position and adds the offset size.</param>
        public void JumpTo(long position, bool absolute = false)
            => BaseStream.Position = absolute ? position + Offset : position;

        /// <summary>
        /// Jumps ahead in the stream by the specified amount of bytes.
        /// </summary>
        /// <param name="amount">Byte count.</param>
        public void JumpAhead(long amount = 1)
            => BaseStream.Position += amount;

        /// <summary>
        /// Jumps behind in the stream by the specified amount of bytes.
        /// </summary>
        /// <param name="amount">Byte count.</param>
        public void JumpBehind(long amount = 1)
            => BaseStream.Position -= amount;

        /// <summary>
        /// Fixes padding in the stream.
        /// </summary>
        /// <param name="amount">Padding count.</param>
        public void FixPadding(uint amount = 4)
        {
            if (amount > 1)
            {
                long jumpAmount = 0;

                while ((BaseStream.Position + jumpAmount) % amount != 0) jumpAmount++;

                JumpAhead(jumpAmount);
            }
        }

        /// <summary>
        /// Returns a string at the current position.
        /// </summary>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="isNullTerminated">Is the string null-terminated?</param>
        public string GetString(bool additive = false, bool isNullTerminated = true)
        {
            uint offset = additive ? ReadUInt32() + Offset : ReadUInt32();

            return GetString(offset, isNullTerminated);
        }

        /// <summary>
        /// Returns a string at the current position.
        /// </summary>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="isNullTerminated">Is the string null-terminated?</param>
        public string GetString(uint offset, bool isNullTerminated = true)
        {
            long position = BaseStream.Position;
            BaseStream.Position = offset;

            string str = isNullTerminated ? ReadNullTerminatedString() : ReadString();

            BaseStream.Position = position;

            return str;
        }

        /// <summary>
        /// Reads the signature at the current position.
        /// </summary>
        /// <param name="length">Signature length.</param>
        public string ReadSignature(int length = 4)
            => Encoding.ASCII.GetString(ReadBytes(length));

        /// <summary>
        /// Reads a null-terminated UTF-8 string at the current position.
        /// </summary>
        public string ReadNullTerminatedString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Stream stream = BaseStream;
            char currentChar;

            while (stream.Position < stream.Length)
            {
                currentChar = ReadChar();
                if (currentChar == _Null) break;
                stringBuilder.Append(currentChar);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Reads a null-terminated UTF-16 string at the current position.
        /// </summary>
        public string ReadNullTerminatedStringUTF16()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Encoding utf16 = IsBigEndian ? Encoding.BigEndianUnicode : Encoding.Unicode;
            Stream stream = BaseStream;

            while (stream.Position < stream.Length)
            {
                FillBuffer(2);

                if (_Buffer[0] == 0 && _Buffer[1] == 0) break;
                stringBuilder.Append(utf16.GetChars(_Buffer, 0, 2));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the type from the current position.
        /// </summary>
        public T ReadByType<T>()
            => (T)ReadByType(typeof(T));

        /// <summary>
        /// Gets the type from the current position.
        /// </summary>
        /// <param name="type">Object data type.</param>
        /// <param name="throwExceptionWhenUnknown">Throw an exception if the type is unknown?</param>
        public object ReadByType(Type type, bool throwExceptionWhenUnknown = true)
        {
            if (type == typeof(bool))
                return ReadBoolean();
            else if (type == typeof(byte))
                return ReadByte();
            else if (type == typeof(sbyte))
                return ReadSByte();
            else if (type == typeof(char))
                return ReadChar();
            else if (type == typeof(short))
                return ReadInt16();
            else if (type == typeof(ushort))
                return ReadUInt16();
            else if (type == typeof(int))
                return ReadInt32();
            else if (type == typeof(uint))
                return ReadUInt32();
            else if (type == typeof(long))
                return ReadInt64();
            else if (type == typeof(ulong))
                return ReadUInt64();
            else if (type == typeof(Half))
                return ReadHalf();
            else if (type == typeof(float))
                return ReadSingle();
            else if (type == typeof(double))
                return ReadDouble();
            else if (type == typeof(Vector2))
                return ReadVector2();
            else if (type == typeof(Vector3))
                return ReadVector3();
            else if (type == typeof(Vector4))
                return ReadVector4();
            else if (type == typeof(Quaternion))
                return ReadQuaternion();
            else if (type == typeof(string))
                return ReadString();

            if (throwExceptionWhenUnknown)
                throw new NotImplementedException($"Reading \"{type}\" is not implemented yet!");

            return null;
        }

        /// <summary>
        /// Reads a Boolean from the current position.
        /// </summary>
        public override bool ReadBoolean()
            => ReadByte() != 0;

        /// <summary>
        /// Reads a Boolean from the current position.
        /// </summary>
        public virtual bool ReadBoolean32()
            => ReadInt32() != 0;

        /// <summary>
        /// Reads a byte from the current position.
        /// </summary>
        public override byte ReadByte()
        {
            if (_Stream == null)
                throw new ObjectDisposedException("stream", _StreamClosed);

            int @byte = _Stream.ReadByte();

            if (@byte == -1)
                throw new EndOfStreamException(_EndOfStream);

            return (byte)@byte;
        }

        /// <summary>
        /// Reads a signed byte from the current position.
        /// </summary>
        public override sbyte ReadSByte()
        {
            if (_Stream == null)
                throw new ObjectDisposedException("stream", _StreamClosed);

            int @sbyte = _Stream.ReadByte();

            if (@sbyte == -1)
                throw new EndOfStreamException(_EndOfStream);

            return (sbyte)@sbyte;
        }

        /// <summary>
        /// Reads two bytes from the current position as a half-precision floating point value.
        /// </summary>
        public Half ReadHalf()
            => Half.ToHalf(ReadUInt16());

        /// <summary>
        /// Reads an Int16 from the current position.
        /// </summary>
        public override unsafe short ReadInt16()
        {
            FillBuffer(sizeof(short));

            return IsBigEndian ?

                   // Big-endian
                   (short)(_Buffer[0] << 8 | _Buffer[1]) :
                
                   // Little-endian
                   (short)(_Buffer[1] << 8 | _Buffer[0]);
        }

        /// <summary>
        /// Reads a UInt16 from the current position.
        /// </summary>
        public override ushort ReadUInt16()
        {
            FillBuffer(sizeof(ushort));

            return IsBigEndian ?

                   // Big-endian
                   (ushort)(_Buffer[0] << 8 | _Buffer[1]) :
                
                   // Little-endian
                   (ushort)(_Buffer[1] << 8 | _Buffer[0]);
        }

        /// <summary>
        /// Reads an Int24 from the current position.
        /// </summary>
        public unsafe int ReadInt24()
        {
            FillBuffer(3);

            return IsBigEndian ?

                   // Big-endian
                   _Buffer[0] << 16 | _Buffer[1] << 8 | _Buffer[2] :

                   // Little-endian
                   _Buffer[2] << 16 | _Buffer[1] << 8 | _Buffer[0];
        }

        /// <summary>
        /// Reads a UInt24 from the current position.
        /// </summary>
        public unsafe uint ReadUInt24()
        {
            FillBuffer(3);

            return IsBigEndian ?

                   // Big-endian
                   ((uint)_Buffer[0] << 16 | (uint)_Buffer[1] << 8 | _Buffer[2]) :

                   // Little-endian
                   ((uint)_Buffer[2] << 16 | (uint)_Buffer[1] << 8 | _Buffer[0]);
        }

        /// <summary>
        /// Reads an Int32 from the current position.
        /// </summary>
        public override int ReadInt32()
        {
            FillBuffer(sizeof(int));

            return IsBigEndian ?

                   // Big-endian
                   _Buffer[0] << 24 | _Buffer[1] << 16 | _Buffer[2] << 8 | _Buffer[3] :

                   // Little-endian
                   _Buffer[3] << 24 | _Buffer[2] << 16 | _Buffer[1] << 8 | _Buffer[0];
        }

        /// <summary>
        /// Reads a UInt32 from the current position.
        /// </summary>
        public override uint ReadUInt32()
        {
            FillBuffer(sizeof(uint));

            return IsBigEndian ?

                   // Big-endian
                   ((uint)_Buffer[0] << 24 | (uint)_Buffer[1] << 16 | (uint)_Buffer[2] << 8 | _Buffer[3]):

                   // Little-endian
                   ((uint)_Buffer[3] << 24 | (uint)_Buffer[2] << 16 | (uint)_Buffer[1] << 8 | _Buffer[0]);
        }

        /// <summary>
        /// Reads a Single from the current position.
        /// </summary>
        public override unsafe float ReadSingle()
        {
            uint @single = ReadUInt32();

            return *(float*)&@single;
        }

        /// <summary>
        /// Reads an Int64 from the current position.
        /// </summary>
        public override long ReadInt64()
        {
            FillBuffer(sizeof(long));

            return IsBigEndian ?

                   // Big-endian
                   ((long)_Buffer[0] << 56 | (long)_Buffer[1] << 48 | (long)_Buffer[2] << 40 | (long)_Buffer[3] << 32 |
                   (long)_Buffer[4] << 24 | (long)_Buffer[5] << 16 | (long)_Buffer[6] << 8 | _Buffer[7]) :

                   // Little-endian
                   ((long)_Buffer[7] << 56 | (long)_Buffer[6] << 48 | (long)_Buffer[5] << 40 | (long)_Buffer[4] << 32 |
                   (long)_Buffer[3] << 24 | (long)_Buffer[2] << 16 | (long)_Buffer[1] << 8 | _Buffer[0]);
        }

        /// <summary>
        /// Reads a UInt64 from the current position.
        /// </summary>
        public override ulong ReadUInt64()
        {
            FillBuffer(sizeof(ulong));

            return IsBigEndian ?

                   // Big-endian
                   ((ulong)_Buffer[0] << 56 | (ulong)_Buffer[1] << 48 | (ulong)_Buffer[2] << 40 | (ulong)_Buffer[3] << 32 |
                    (ulong)_Buffer[4] << 24 | (ulong)_Buffer[5] << 16 | (ulong)_Buffer[6] << 8 | _Buffer[7]) :

                   // Little-endian
                   ((ulong)_Buffer[7] << 56 | (ulong)_Buffer[6] << 48 | (ulong)_Buffer[5] << 40 | (ulong)_Buffer[4] << 32 |
                    (ulong)_Buffer[3] << 24 | (ulong)_Buffer[2] << 16 | (ulong)_Buffer[1] << 8 | _Buffer[0]);
        }

        /// <summary>
        /// Reads a Double from the current position.
        /// </summary>
        public override unsafe double ReadDouble()
        {
            ulong @double = ReadUInt64();

            return *(double*)&@double;
        }

        /// <summary>
        /// Reads a Vector2 from the current position.
        /// </summary>
        public virtual unsafe Vector2 ReadVector2()
        {
            var vec = new Vector2();

            uint v = ReadUInt32();
            vec.X = *(float*)&v;

            v = ReadUInt32();
            vec.Y = *(float*)&v;

            return vec;
        }

        /// <summary>
        /// Reads a Vector3 from the current position.
        /// </summary>
        public virtual unsafe Vector3 ReadVector3()
        {
            var vec = new Vector3();

            uint v = ReadUInt32();
            vec.X = *(float*)&v;

            v = ReadUInt32();
            vec.Y = *(float*)&v;

            v = ReadUInt32();
            vec.Z = *(float*)&v;

            return vec;
        }

        /// <summary>
        /// Reads a Vector4 from the current position.
        /// </summary>
        public virtual Vector4 ReadVector4()
        {
            var vec = new Vector4();

            ReadVector4(vec);

            return vec;
        }

        /// <summary>
        /// Reads a Quaternion from the current position.
        /// </summary>
        public virtual Quaternion ReadQuaternion()
        {
            var vec = new Quaternion();

            ReadVector4(vec);

            return vec;
        }

        /// <summary>
        /// Reads a Vector4 from the current position.
        /// </summary>
        /// <param name="vect">Vector4 to read.</param>
        protected unsafe virtual void ReadVector4(Vector4 vect)
        {
            uint v = ReadUInt32();
            vect.X = *(float*)&v;

            v = ReadUInt32();
            vect.Y = *(float*)&v;

            v = ReadUInt32();
            vect.Z = *(float*)&v;

            v = ReadUInt32();
            vect.W = *(float*)&v;
        }

        /// <summary>
        /// Fills the stream buffer.
        /// </summary>
        protected override void FillBuffer(int numBytes)
        {
            int n, bytesRead = 0;

            if (_Stream == null)
                throw new ObjectDisposedException("stream", _StreamClosed);

            if (numBytes == 1)
            {
                n = _Stream.ReadByte();

                if (n == -1)
                    throw new EndOfStreamException(_EndOfStream);

                _Buffer[0] = (byte)n;
                return;
            }

            while (numBytes > 0)
            {
                n = _Stream.Read(_Buffer, bytesRead, numBytes);

                if (n == 0)
                    throw new EndOfStreamException(_EndOfStream);

                bytesRead += n;
                numBytes -= n;
            }
        }

        /// <summary>
        /// Disposes the stream and buffer.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && _Stream != null)
                _Stream.Close();

            _Stream = null;
            _Buffer = null;
        }
    }

    public class ExtendedBinaryWriter : BinaryWriter
    {
        public uint Offset = 0;
        public bool IsBigEndian = false;

        protected byte[] _DataBuffer = new byte[_BufferSize];

        protected Encoding _Encoding;
        protected const uint _BufferSize = 16;
        protected Dictionary<string, uint> _OffsetDictionary = new Dictionary<string, uint>();

        public ExtendedBinaryWriter(Stream output, bool isBigEndian = false) : this(output, Encoding.ASCII, false)
            => IsBigEndian = isBigEndian;

        public ExtendedBinaryWriter(Stream output, Encoding encoding, bool isBigEndian = false) : base(output, encoding, false)
        {
            IsBigEndian = isBigEndian;
            _Encoding = encoding;
        }

        /// <summary>
        /// Adds an offset to the dictionary and overwrites any duplicates.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="offsetLength">Length of dictionary entry offset.</param>
        public virtual void AddOffset(string name, uint offsetLength = 4)
        {
            if (_OffsetDictionary.ContainsKey(name))
                _OffsetDictionary[name] = (uint)BaseStream.Position;
            else
                _OffsetDictionary.Add(name, (uint)BaseStream.Position);

            WriteNulls(offsetLength);
        }

        /// <summary>
        /// Adds an offset table to the dictionary.
        /// </summary>
        /// <param name="namePrefix">Name of offset table.</param>
        /// <param name="offsetCount">Number of offsets.</param>
        /// <param name="offsetLength">Length of offsets.</param>
        public void AddOffsetTable(string namePrefix, uint offsetCount, uint offsetLength = 4)
        {
            for (uint i = 0; i < offsetCount; ++i)
                AddOffset($"{namePrefix}_{i}", offsetLength);
        }

        /// <summary>
        /// Jumps to an offset in the dictionary.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void JumpToOffset(string name, bool removeOffset = true)
        {
            OutStream.Position = _OffsetDictionary[name];

            if (removeOffset)
                _OffsetDictionary.Remove(name);
        }

        /// <summary>
        /// Returns an offset from the dictionary.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        public virtual long ReadOffsetPosition(string name)
            => _OffsetDictionary[name];

        /// <summary>
        /// Writes data to a previously set dictionary entry.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void FillInOffset(string name, bool additive = false, bool removeOffset = true, bool throwOnMissingOffset = true)
        {
            try
            {
                long position = OutStream.Position;

                WriteOffsetValueAtPosition(_OffsetDictionary[name], (uint)position, additive);

                if (removeOffset)
                    _OffsetDictionary.Remove(name);

                OutStream.Position = position;
            }
            catch
            {
                if (throwOnMissingOffset)
                    throw new Exception("The specified offset is not part of the dictionary!");
            }
        }

        /// <summary>
        /// Writes data to a previously set dictionary entry.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="value">Value to write at the offset.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void FillInOffset(string name, uint value, bool additive = false, bool removeOffset = true, bool throwOnMissingOffset = true)
        {
            try
            {
                long position = OutStream.Position;

                WriteOffsetValueAtPosition(_OffsetDictionary[name], value, additive);

                if (removeOffset)
                    _OffsetDictionary.Remove(name);

                OutStream.Position = position;
            }
            catch
            {
                if (throwOnMissingOffset)
                    throw new Exception("The specified offset is not part of the dictionary!");
            }
        }

        /// <summary>
        /// Writes data to a previously set dictionary entry.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="value">Value to write at the offset.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void FillInOffset(string name, ulong value, bool additive = false, bool removeOffset = true, bool throwOnMissingOffset = true)
        {
            try
            {
                long position = OutStream.Position;

                WriteOffsetValueAtPosition(_OffsetDictionary[name], value, additive);

                if (removeOffset)
                    _OffsetDictionary.Remove(name);

                OutStream.Position = position;
            }
            catch
            {
                if (throwOnMissingOffset)
                    throw new Exception("The specified offset is not part of the dictionary!");
            }
        }

        /// <summary>
        /// Writes data to a previously set dictionary entry.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void FillInOffsetLong(string name, bool additive = false, bool removeOffset = true, bool throwOnMissingOffset = true)
        {
            try
            {
                long position = OutStream.Position;

                WriteOffsetValueAtPosition(_OffsetDictionary[name], (ulong)position, additive);

                if (removeOffset)
                    _OffsetDictionary.Remove(name);

                OutStream.Position = position;
            }
            catch
            {
                if (throwOnMissingOffset)
                    throw new Exception("The specified offset is not part of the dictionary!");
            }
        }

        /// <summary>
        /// Writes the offset value at the specified position.
        /// </summary>
        /// <param name="pos">Stream position.</param>
        /// <param name="value">Value to write at the offset.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        protected virtual void WriteOffsetValueAtPosition(uint pos, uint value, bool additive = false)
        {
            OutStream.Position = pos;
            Write(additive ? value - Offset : value);
        }

        /// <summary>
        /// Writes the offset value at the specified position.
        /// </summary>
        /// <param name="pos">Stream position.</param>
        /// <param name="value">Value to write at the offset.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        protected virtual void WriteOffsetValueAtPosition(long pos, ulong value, bool additive = false)
        {
            OutStream.Position = pos;
            Write(additive ? value - Offset : value);
        }

        /// <summary>
        /// Writes a null byte at the current position.
        /// </summary>
        public void WriteNull()
            => OutStream.WriteByte(0);

        /// <summary>
        /// Writes a series of null bytes at the current position.
        /// </summary>
        /// <param name="count">Number of null bytes.</param>
        public void WriteNulls(uint count)
            => Write(new byte[count]);

        /// <summary>
        /// Writes a null-terminated UTF-8 string at the current position.
        /// </summary>
        /// <param name="value">Text to write.</param>
        public void WriteNullTerminatedString(string value)
        {
            Write(_Encoding.GetBytes(value));
            OutStream.WriteByte(0);
        }

        /// <summary>
        /// Writes a null-terminated UTF-16 string at the current position.
        /// </summary>
        /// <param name="value">Text to write.</param>
        public void WriteNullTerminatedStringUTF16(string value)
        {
            Encoding utf16 = IsBigEndian ? Encoding.BigEndianUnicode : Encoding.Unicode;

            Write(utf16.GetBytes(value));

            _DataBuffer[0] = _DataBuffer[1] = 0;

            OutStream.Write(_DataBuffer, 0, sizeof(ushort));
        }

        /// <summary>
        /// Writes a string padded with null characters.
        /// </summary>
        public void WriteNullPaddedString(string text, int length)
        {
            if (text.Length < length)
            {
                throw new ArgumentOutOfRangeException("text", $"The string was longer than {length} characters.");
            }
            else
            {
                Write(text.PadRight(length, '\0'));
            }
        }

        /// <summary>
        /// Fixes padding in the stream.
        /// </summary>
        /// <param name="amount">Padding count.</param>
        public void FixPadding(uint amount = 4)
        {
            if (amount > 1)
            {
                uint padAmount = 0;

                while ((OutStream.Position + padAmount) % amount != 0) padAmount++;

                WriteNulls(padAmount);
            }
        }

        /// <summary>
        /// Writes the specified signature to the stream.
        /// </summary>
        /// <param name="signature">File signature.</param>
        public void WriteSignature(string signature)
            => Write(_Encoding.GetBytes(signature));

        /// <summary>
        /// Writes the type from the current position.
        /// </summary>
        /// <param name="data">Object data.</param>
        public void WriteByType<T>(object data)
            => WriteByType(typeof(T), data);

        /// <summary>
        /// Gets the type from the current position.
        /// </summary>
        /// <param name="type">Object data type.</param>
        /// <param name="data">Object data.</param>
        /// <param name="throwExceptionWhenUnknown">Throw an exception if the type is unknown?</param>
        public bool WriteByType(Type type, object data, bool throwExceptionWhenUnknown = true)
        {
            if (type == typeof(bool))
                Write((bool)data);
            else if (type == typeof(byte))
                Write((byte)data);
            else if (type == typeof(sbyte))
                Write((sbyte)data);
            else if (type == typeof(char))
                Write((char)data);
            else if (type == typeof(short))
                Write((short)data);
            else if (type == typeof(ushort))
                Write((ushort)data);
            else if (type == typeof(int))
                Write((int)data);
            else if (type == typeof(uint))
                Write((uint)data);
            else if (type == typeof(long))
                Write((long)data);
            else if (type == typeof(ulong))
                Write((ulong)data);
            else if (type == typeof(Half))
                Write((Half)data);
            else if (type == typeof(float))
                Write((float)data);
            else if (type == typeof(double))
                Write((double)data);
            else if (type == typeof(Vector2))
                Write((Vector2)data);
            else if (type == typeof(Vector3))
                Write((Vector3)data);
            else if (type == typeof(Vector4) || type == typeof(Quaternion))
                Write((Vector4)data);
            else if (type == typeof(string))
                Write((string)data);
            else
            {
                if (throwExceptionWhenUnknown)
                    throw new NotImplementedException($"Writing \"{type}\" is not implemented yet!");

                return false;
            }

            return true;
        }

        /// <summary>
        /// Writes a Half to the current position.
        /// </summary>
        public void WriteHalf(Half value)
            => Write(value.value);

        /// <summary>
        /// Writes a Boolean value with Int32 alignment to the stream.
        /// </summary>
        /// <param name="value"></param>
        public void WriteBoolean32(bool value)
        {
            if (value)
                WriteByType<int>(1);

            else
                WriteByType<int>(0);
        }

        /// <summary>
        /// Writes an Int24 to the current position.
        /// </summary>
        public void WriteInt24(int value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 16);
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)(value >> 16);
            }

            OutStream.Write(_DataBuffer, 0, 3);
        }

        /// <summary>
        /// Writes a UInt24 to the current position.
        /// </summary>
        public void WriteUInt24(uint value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 16);
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)(value >> 16);
            }

            OutStream.Write(_DataBuffer, 0, 3);
        }

        /// <summary>
        /// Writes a string to the current position.
        /// </summary>
        public override void Write(string value)
            => Write(_Encoding.GetBytes(value));

        /// <summary>
        /// Writes an Int16 to the current position.
        /// </summary>
        public override void Write(short value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 8);
                _DataBuffer[1] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
            }

            OutStream.Write(_DataBuffer, 0, sizeof(short));
        }

        /// <summary>
        /// Writes a UInt16 to the current position.
        /// </summary>
        public override void Write(ushort value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 8);
                _DataBuffer[1] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
            }

            OutStream.Write(_DataBuffer, 0, sizeof(ushort));
        }

        /// <summary>
        /// Writes an Int32 to the current position.
        /// </summary>
        public override void Write(int value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 24);
                _DataBuffer[1] = (byte)(value >> 16);
                _DataBuffer[2] = (byte)(value >> 8);
                _DataBuffer[3] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)(value >> 16);
                _DataBuffer[3] = (byte)(value >> 24);
            }

            OutStream.Write(_DataBuffer, 0, sizeof(int));
        }

        /// <summary>
        /// Writes a UInt32 to the current position.
        /// </summary>
        public override void Write(uint value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 24);
                _DataBuffer[1] = (byte)(value >> 16);
                _DataBuffer[2] = (byte)(value >> 8);
                _DataBuffer[3] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)(value >> 16);
                _DataBuffer[3] = (byte)(value >> 24);
            }

            OutStream.Write(_DataBuffer, 0, sizeof(uint));
        }

        /// <summary>
        /// Writes an Int64 to the current position.
        /// </summary>
        public override void Write(long value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 56);
                _DataBuffer[1] = (byte)(value >> 48);
                _DataBuffer[2] = (byte)(value >> 40);
                _DataBuffer[3] = (byte)(value >> 32);

                _DataBuffer[4] = (byte)(value >> 24);
                _DataBuffer[5] = (byte)(value >> 16);
                _DataBuffer[6] = (byte)(value >> 8);
                _DataBuffer[7] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)(value >> 16);
                _DataBuffer[3] = (byte)(value >> 24);

                _DataBuffer[4] = (byte)(value >> 32);
                _DataBuffer[5] = (byte)(value >> 40);
                _DataBuffer[6] = (byte)(value >> 48);
                _DataBuffer[7] = (byte)(value >> 56);
            }

            OutStream.Write(_DataBuffer, 0, sizeof(long));
        }

        /// <summary>
        /// Writes a UInt64 to the current position.
        /// </summary>
        public override void Write(ulong value)
        {
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(value >> 56);
                _DataBuffer[1] = (byte)(value >> 48);
                _DataBuffer[2] = (byte)(value >> 40);
                _DataBuffer[3] = (byte)(value >> 32);

                _DataBuffer[4] = (byte)(value >> 24);
                _DataBuffer[5] = (byte)(value >> 16);
                _DataBuffer[6] = (byte)(value >> 8);
                _DataBuffer[7] = (byte)value;
            }
            else
            {
                _DataBuffer[0] = (byte)value;
                _DataBuffer[1] = (byte)(value >> 8);
                _DataBuffer[2] = (byte)(value >> 16);
                _DataBuffer[3] = (byte)(value >> 24);

                _DataBuffer[4] = (byte)(value >> 32);
                _DataBuffer[5] = (byte)(value >> 40);
                _DataBuffer[6] = (byte)(value >> 48);
                _DataBuffer[7] = (byte)(value >> 56);
            }

            OutStream.Write(_DataBuffer, 0, sizeof(ulong));
        }

        /// <summary>
        /// Writes a Single to the current position.
        /// </summary>
        public override unsafe void Write(float value)
            => Write(*(uint*)&value);

        /// <summary>
        /// Writes a Double to the current position.
        /// </summary>
        public override unsafe void Write(double value)
            => Write(*(ulong*)&value);

        /// <summary>
        /// Writes a Vector2 to the current position.
        /// </summary>
        /// <param name="vect">Vector2 to write.</param>
        public virtual unsafe void Write(Vector2 vect)
        {
            var p = (uint*)&vect.X;
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(*p >> 24);
                _DataBuffer[1] = (byte)(*p >> 16);
                _DataBuffer[2] = (byte)(*p >> 8);
                _DataBuffer[3] = (byte)(*p);

                p = ((uint*)&vect.Y);
                _DataBuffer[4] = (byte)(*p >> 24);
                _DataBuffer[5] = (byte)(*p >> 16);
                _DataBuffer[6] = (byte)(*p >> 8);
                _DataBuffer[7] = (byte)(*p);
            }
            else
            {
                _DataBuffer[0] = (byte)(*p);
                _DataBuffer[1] = (byte)(*p >> 8);
                _DataBuffer[2] = (byte)(*p >> 16);
                _DataBuffer[3] = (byte)(*p >> 24);

                p = ((uint*)&vect.Y);
                _DataBuffer[4] = (byte)(*p);
                _DataBuffer[5] = (byte)(*p >> 8);
                _DataBuffer[6] = (byte)(*p >> 16);
                _DataBuffer[7] = (byte)(*p >> 24);
            }

            Write(_DataBuffer, 0, 8);
        }

        /// <summary>
        /// Writes a Vector3 to the current position.
        /// </summary>
        /// <param name="vect">Vector3 to write.</param>
        public virtual unsafe void Write(Vector3 vect)
        {
            var p = (uint*)&vect.X;
            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(*p >> 24);
                _DataBuffer[1] = (byte)(*p >> 16);
                _DataBuffer[2] = (byte)(*p >> 8);
                _DataBuffer[3] = (byte)(*p);

                p = ((uint*)&vect.Y);
                _DataBuffer[4] = (byte)(*p >> 24);
                _DataBuffer[5] = (byte)(*p >> 16);
                _DataBuffer[6] = (byte)(*p >> 8);
                _DataBuffer[7] = (byte)(*p);

                p = ((uint*)&vect.Z);
                _DataBuffer[8] = (byte)(*p >> 24);
                _DataBuffer[9] = (byte)(*p >> 16);
                _DataBuffer[10] = (byte)(*p >> 8);
                _DataBuffer[11] = (byte)(*p);
            }
            else
            {
                _DataBuffer[0] = (byte)(*p);
                _DataBuffer[1] = (byte)(*p >> 8);
                _DataBuffer[2] = (byte)(*p >> 16);
                _DataBuffer[3] = (byte)(*p >> 24);

                p = ((uint*)&vect.Y);
                _DataBuffer[4] = (byte)(*p);
                _DataBuffer[5] = (byte)(*p >> 8);
                _DataBuffer[6] = (byte)(*p >> 16);
                _DataBuffer[7] = (byte)(*p >> 24);

                p = ((uint*)&vect.Z);
                _DataBuffer[8] = (byte)(*p);
                _DataBuffer[9] = (byte)(*p >> 8);
                _DataBuffer[10] = (byte)(*p >> 16);
                _DataBuffer[11] = (byte)(*p >> 24);
            }

            Write(_DataBuffer, 0, 12);
        }

        /// <summary>
        /// Writes a Vector4 to the current position.
        /// </summary>
        /// <param name="vect">Vector4 to write.</param>
        public virtual unsafe void Write(Vector4 vect)
        {
            float f = vect.X;
            var p = (uint*)&f;

            if (IsBigEndian)
            {
                _DataBuffer[0] = (byte)(*p >> 24);
                _DataBuffer[1] = (byte)(*p >> 16);
                _DataBuffer[2] = (byte)(*p >> 8);
                _DataBuffer[3] = (byte)(*p);

                f = vect.Y;
                _DataBuffer[4] = (byte)(*p >> 24);
                _DataBuffer[5] = (byte)(*p >> 16);
                _DataBuffer[6] = (byte)(*p >> 8);
                _DataBuffer[7] = (byte)(*p);

                f = vect.Z;
                _DataBuffer[8] = (byte)(*p >> 24);
                _DataBuffer[9] = (byte)(*p >> 16);
                _DataBuffer[10] = (byte)(*p >> 8);
                _DataBuffer[11] = (byte)(*p);

                f = vect.W;
                _DataBuffer[12] = (byte)(*p >> 24);
                _DataBuffer[13] = (byte)(*p >> 16);
                _DataBuffer[14] = (byte)(*p >> 8);
                _DataBuffer[15] = (byte)(*p);
            }
            else
            {
                _DataBuffer[0] = (byte)(*p);
                _DataBuffer[1] = (byte)(*p >> 8);
                _DataBuffer[2] = (byte)(*p >> 16);
                _DataBuffer[3] = (byte)(*p >> 24);

                f = vect.Y;
                _DataBuffer[4] = (byte)(*p);
                _DataBuffer[5] = (byte)(*p >> 8);
                _DataBuffer[6] = (byte)(*p >> 16);
                _DataBuffer[7] = (byte)(*p >> 24);

                f = vect.Z;
                _DataBuffer[8] = (byte)(*p);
                _DataBuffer[9] = (byte)(*p >> 8);
                _DataBuffer[10] = (byte)(*p >> 16);
                _DataBuffer[11] = (byte)(*p >> 24);

                f = vect.W;
                _DataBuffer[12] = (byte)(*p);
                _DataBuffer[13] = (byte)(*p >> 8);
                _DataBuffer[14] = (byte)(*p >> 16);
                _DataBuffer[15] = (byte)(*p >> 24);
            }

            Write(_DataBuffer, 0, 16);
        }
    }
}
