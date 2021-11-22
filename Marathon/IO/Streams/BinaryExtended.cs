using Marathon.Exceptions;

namespace Marathon.IO
{
    public class BinaryReaderEx : BinaryReader
    {
        public uint Offset = 0;
        public bool IsBigEndian = false;

        protected Stream Stream;
        protected byte[] Buffer;

        protected const int MinimumBufferSize = 16;

        public BinaryReaderEx(Stream input, bool isBigEndian = false) : this(input, Encoding.ASCII, isBigEndian) { }

        public BinaryReaderEx(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, false)
        {
            Stream = input;
            IsBigEndian = isBigEndian;

            int bufferSize = encoding.GetMaxByteCount(1);

            if (bufferSize < MinimumBufferSize)
                bufferSize = MinimumBufferSize;

            Buffer = new byte[bufferSize];
        }

        /// <summary>
        /// Jumps to a position in the stream.
        /// </summary>
        /// <param name="position">Position to jump to.</param>
        /// <param name="additive">Jumps to the specified position and adds the offset size.</param>
        public void JumpTo(long position, bool additive = false) => BaseStream.Position = additive ? position + Offset : position;

        /// <summary>
        /// Jumps ahead in the stream by the specified amount of bytes.
        /// </summary>
        /// <param name="amount">Byte count.</param>
        public void JumpAhead(long amount = 1) => BaseStream.Position += amount;

        /// <summary>
        /// Jumps behind in the stream by the specified amount of bytes.
        /// </summary>
        /// <param name="amount">Byte count.</param>
        public void JumpBehind(long amount = 1) => BaseStream.Position -= amount;

        /// <summary>
        /// Fixes padding in the stream.
        /// </summary>
        /// <param name="amount">Padding count.</param>
        public void FixPadding(uint amount = 4)
        {
            if (amount > 1)
            {
                long jumpAmount = 0;

                while ((BaseStream.Position + jumpAmount) % amount != 0)
                    jumpAmount++;

                JumpAhead(jumpAmount);
            }
        }

        /// <summary>
        /// Reads the signature at the current position.
        /// </summary>
        /// <param name="length">Signature length.</param>
        /// <param name="expectedSignature">The expected result for the signature.</param>
        /// <param name="throwOnInvalid">Throw an exception if the signature is invalid.</param>
        public void ReadSignature(int length, byte[] expectedSignature, bool throwOnInvalid = true)
        {
            byte[] receivedSignature = ReadBytes(length);

            if (!expectedSignature.SequenceEqual(receivedSignature) && throwOnInvalid)
                throw new InvalidSignatureException(Encoding.ASCII.GetString(expectedSignature), Encoding.ASCII.GetString(receivedSignature));
        }

        /// <summary>
        /// Reads the signature at the current position as a string.
        /// </summary>
        /// <param name="length">Signature length.</param>
        /// <param name="expectedSignature">The expected result for the signature.</param>
        /// <param name="throwOnInvalid">Throw an exception if the signature is invalid.</param>
        public void ReadSignature(int length, string expectedSignature, bool throwOnInvalid = true)
            => ReadSignature(length, Encoding.ASCII.GetBytes(expectedSignature), throwOnInvalid);

        /// <summary>
        /// Reads the signature at the current position as a UInt32.
        /// </summary>
        /// <param name="expectedSignature">The expected result for the signature.</param>
        /// <param name="throwOnInvalid">Throw an exception if the signature is invalid.</param>
        public void ReadSignature(uint expectedSignature, bool throwOnInvalid = true)
        {
            uint receivedSignature = ReadUInt32();

            if (receivedSignature != expectedSignature && throwOnInvalid)
                throw new InvalidSignatureException(expectedSignature.ToString("X"), receivedSignature.ToString("X"));
        }

        /// <summary>
        /// Reads a null-terminated string at the current position.
        /// </summary>
        public string ReadNullTerminatedString(bool isUTF16 = false, long position = -1, bool additive = false)
        {
            StringBuilder stringBuilder = new();
            Encoding utf16 = IsBigEndian ? Encoding.BigEndianUnicode : Encoding.Unicode;
            Stream stream = BaseStream;
            char currentChar;

            if (position != -1)
                JumpTo(position, additive);

            while (stream.Position < stream.Length)
            {
                if (isUTF16)
                {
                    FillBuffer(2);

                    if (Buffer[0] == 0 && Buffer[1] == 0)
                        break;

                    stringBuilder.Append(utf16.GetChars(Buffer, 0, 2));
                }
                else
                {
                    currentChar = ReadChar();

                    if (currentChar == '\0')
                        break;

                    stringBuilder.Append(currentChar);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Reads a string padded by null characters with a specified length.
        /// </summary>
        /// <param name="count">Length of the string (including null characters).</param>
        public string ReadNullPaddedString(int count)
            => new string(ReadChars(count)).Trim('\0');

        /// <summary>
        /// Reads a Boolean from the current position.
        /// </summary>
        public bool ReadBoolean(int length = 1)
        {
            byte[] buffer = ReadBytes(length);
            int sum = 0;

            foreach (byte b in buffer)
                sum += b;

            return sum != 0;
        }

        /// <summary>
        /// Reads a byte from the current position.
        /// </summary>
        public override byte ReadByte()
        {
            int @byte = Stream.ReadByte();

            if (@byte == -1)
                throw new EndOfStreamException();

            return (byte)@byte;
        }

        /// <summary>
        /// Reads a signed byte from the current position.
        /// </summary>
        public override sbyte ReadSByte()
        {
            int @sbyte = Stream.ReadByte();

            if (@sbyte == -1)
                throw new EndOfStreamException();

            return (sbyte)@sbyte;
        }

        /// <summary>
        /// Reads two bytes from the current position as a half-precision floating point value.
        /// </summary>
        public Half ReadHalf() => (Half)ReadUInt16();

        /// <summary>
        /// Reads an Int16 from the current position.
        /// </summary>
        public override unsafe short ReadInt16()
        {
            FillBuffer(sizeof(short));

            return IsBigEndian ?

                   // Big-endian
                   (short)(Buffer[0] << 8 | Buffer[1]) :
                
                   // Little-endian
                   (short)(Buffer[1] << 8 | Buffer[0]);
        }

        /// <summary>
        /// Reads a UInt16 from the current position.
        /// </summary>
        public override ushort ReadUInt16()
        {
            FillBuffer(sizeof(ushort));

            return IsBigEndian ?

                   // Big-endian
                   (ushort)(Buffer[0] << 8 | Buffer[1]) :
                
                   // Little-endian
                   (ushort)(Buffer[1] << 8 | Buffer[0]);
        }

        /// <summary>
        /// Reads an Int24 from the current position.
        /// </summary>
        public unsafe int ReadInt24()
        {
            FillBuffer(3);

            return IsBigEndian ?

                   // Big-endian
                   Buffer[0] << 16 | Buffer[1] << 8 | Buffer[2] :

                   // Little-endian
                   Buffer[2] << 16 | Buffer[1] << 8 | Buffer[0];
        }

        /// <summary>
        /// Reads a UInt24 from the current position.
        /// </summary>
        public unsafe uint ReadUInt24()
        {
            FillBuffer(3);

            return IsBigEndian ?

                   // Big-endian
                   ((uint)Buffer[0] << 16 | (uint)Buffer[1] << 8 | Buffer[2]) :

                   // Little-endian
                   ((uint)Buffer[2] << 16 | (uint)Buffer[1] << 8 | Buffer[0]);
        }

        /// <summary>
        /// Reads an Int32 from the current position.
        /// </summary>
        public override int ReadInt32()
        {
            FillBuffer(sizeof(int));

            return IsBigEndian ?

                   // Big-endian
                   Buffer[0] << 24 | Buffer[1] << 16 | Buffer[2] << 8 | Buffer[3] :

                   // Little-endian
                   Buffer[3] << 24 | Buffer[2] << 16 | Buffer[1] << 8 | Buffer[0];
        }

        /// <summary>
        /// Reads a UInt32 from the current position.
        /// </summary>
        public override uint ReadUInt32()
        {
            FillBuffer(sizeof(uint));

            return IsBigEndian ?

                   // Big-endian
                   ((uint)Buffer[0] << 24 | (uint)Buffer[1] << 16 | (uint)Buffer[2] << 8 | Buffer[3]):

                   // Little-endian
                   ((uint)Buffer[3] << 24 | (uint)Buffer[2] << 16 | (uint)Buffer[1] << 8 | Buffer[0]);
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
                   ((long)Buffer[0] << 56 | (long)Buffer[1] << 48 | (long)Buffer[2] << 40 | (long)Buffer[3] << 32 |
                   (long)Buffer[4] << 24 | (long)Buffer[5] << 16 | (long)Buffer[6] << 8 | Buffer[7]) :

                   // Little-endian
                   ((long)Buffer[7] << 56 | (long)Buffer[6] << 48 | (long)Buffer[5] << 40 | (long)Buffer[4] << 32 |
                   (long)Buffer[3] << 24 | (long)Buffer[2] << 16 | (long)Buffer[1] << 8 | Buffer[0]);
        }

        /// <summary>
        /// Reads a UInt64 from the current position.
        /// </summary>
        public override ulong ReadUInt64()
        {
            FillBuffer(sizeof(ulong));

            return IsBigEndian ?

                   // Big-endian
                   ((ulong)Buffer[0] << 56 | (ulong)Buffer[1] << 48 | (ulong)Buffer[2] << 40 | (ulong)Buffer[3] << 32 |
                    (ulong)Buffer[4] << 24 | (ulong)Buffer[5] << 16 | (ulong)Buffer[6] << 8 | Buffer[7]) :

                   // Little-endian
                   ((ulong)Buffer[7] << 56 | (ulong)Buffer[6] << 48 | (ulong)Buffer[5] << 40 | (ulong)Buffer[4] << 32 |
                    (ulong)Buffer[3] << 24 | (ulong)Buffer[2] << 16 | (ulong)Buffer[1] << 8 | Buffer[0]);
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
        public virtual unsafe Vector4 ReadVector4()
        {
            var vec = new Vector4();

            uint v = ReadUInt32();
            vec.X = *(float*)&v;

            v = ReadUInt32();
            vec.Y = *(float*)&v;

            v = ReadUInt32();
            vec.Z = *(float*)&v;

            v = ReadUInt32();
            vec.W = *(float*)&v;

            return vec;
        }

        /// <summary>
        /// Reads a Quaternion from the current position.
        /// </summary>
        public virtual unsafe Quaternion ReadQuaternion()
        {
            var quat = new Quaternion();

            uint v = ReadUInt32();
            quat.X = *(float*)&v;

            v = ReadUInt32();
            quat.Y = *(float*)&v;

            v = ReadUInt32();
            quat.Z = *(float*)&v;

            v = ReadUInt32();
            quat.W = *(float*)&v;

            return quat;
        }

        /// <summary>
        /// Fills the stream buffer.
        /// </summary>
        protected override void FillBuffer(int numBytes)
        {
            int n, bytesRead = 0;

            if (numBytes == 1)
            {
                n = Stream.ReadByte();

                if (n == -1)
                    throw new EndOfStreamException();

                Buffer[0] = (byte)n;

                return;
            }

            while (numBytes > 0)
            {
                n = Stream.Read(Buffer, bytesRead, numBytes);

                if (n == 0)
                    throw new EndOfStreamException();

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

            if (disposing && Stream != null)
                Stream.Close();

            Stream = null;
            Buffer = null;
        }
    }

    public class BinaryWriterEx : BinaryWriter
    {
        public uint Offset = 0;
        public bool IsBigEndian = false;

        protected byte[] Buffer = new byte[16];
        protected Encoding Encoding;
        protected Dictionary<string, uint> Offsets = new();

        public BinaryWriterEx(Stream output, bool isBigEndian = false) : this(output, Encoding.ASCII, false) => IsBigEndian = isBigEndian;

        public BinaryWriterEx(Stream output, Encoding encoding, bool isBigEndian = false) : base(output, encoding, false)
        {
            IsBigEndian = isBigEndian;
            Encoding = encoding;
        }

        /// <summary>
        /// Returns a list of offsets written by the writer.
        /// </summary>
        public List<uint> GetOffsets()
        {
            List<uint> offsetList = new();

            foreach (var value in Offsets)
                offsetList.Add(value.Value);

            return offsetList;
        }

        /// <summary>
        /// Adds an offset to the dictionary and overwrites any duplicates.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="offsetLength">Length of dictionary entry offset.</param>
        public virtual void AddOffset(string name, uint offsetLength = 4)
        {
            if (Offsets.ContainsKey(name))
            {
                Offsets[name] = (uint)BaseStream.Position;
            }
            else
            {
                Offsets.Add(name, (uint)BaseStream.Position);
            }

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
            OutStream.Position = Offsets[name];

            if (removeOffset)
                Offsets.Remove(name);
        }

        /// <summary>
        /// Returns an offset from the dictionary.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        public virtual long ReadOffsetPosition(string name) => Offsets[name];

        /// <summary>
        /// Writes data to a previously set dictionary entry.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void FillOffset(string name, bool additive = false, bool removeOffset = true, bool throwOnMissingOffset = true)
        {
            try
            {
                long position = OutStream.Position;

                WriteOffsetValueAtPosition(Offsets[name], (uint)position, additive);

                if (removeOffset)
                    Offsets.Remove(name);

                OutStream.Position = position;
            }
            catch
            {
                if (throwOnMissingOffset)
                    throw new KeyNotFoundException("The specified offset is not part of the dictionary!");
            }
        }

        /// <summary>
        /// Writes data to a previously set dictionary entry.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="value">Value to write at the offset.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void FillOffset(string name, uint value, bool additive = false, bool removeOffset = true, bool throwOnMissingOffset = true)
        {
            try
            {
                long position = OutStream.Position;

                WriteOffsetValueAtPosition(Offsets[name], value, additive);

                if (removeOffset)
                    Offsets.Remove(name);

                OutStream.Position = position;
            }
            catch
            {
                if (throwOnMissingOffset)
                    throw new KeyNotFoundException("The specified offset is not part of the dictionary!");
            }
        }

        /// <summary>
        /// Writes data to a previously set dictionary entry.
        /// </summary>
        /// <param name="name">Name of dictionary entry.</param>
        /// <param name="value">Value to write at the offset.</param>
        /// <param name="additive">Adds the specified offset to the position.</param>
        /// <param name="removeOffset">Removes the offset once filled in.</param>
        public virtual void FillOffset(string name, ulong value, bool additive = false, bool removeOffset = true, bool throwOnMissingOffset = true)
        {
            try
            {
                long position = OutStream.Position;

                WriteOffsetValueAtPosition(Offsets[name], value, additive);

                if (removeOffset)
                    Offsets.Remove(name);

                OutStream.Position = position;
            }
            catch
            {
                if (throwOnMissingOffset)
                    throw new KeyNotFoundException("The specified offset is not part of the dictionary!");
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
        public void WriteNull() => OutStream.WriteByte(0);

        /// <summary>
        /// Writes a series of null bytes at the current position.
        /// </summary>
        /// <param name="count">Number of null bytes.</param>
        public void WriteNulls(uint count) => Write(new byte[count]);

        /// <summary>
        /// Writes a null-terminated UTF-8 string at the current position.
        /// </summary>
        /// <param name="value">Text to write.</param>
        public void WriteNullTerminatedString(string value)
        {
            Write(Encoding.GetBytes(value));

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

            Buffer[0] = Buffer[1] = 0;

            OutStream.Write(Buffer, 0, sizeof(ushort));
        }

        /// <summary>
        /// Writes a string padded with null characters.
        /// </summary>
        public void WriteNullPaddedString(string text, int length)
        {
            if (text.Length > length)
                throw new ArgumentOutOfRangeException(nameof(text), $"The string was longer than {length} characters.");

            Write(text.PadRight(length, '\0'));
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

                while ((OutStream.Position + padAmount) % amount != 0)
                    padAmount++;

                WriteNulls(padAmount);
            }
        }

        /// <summary>
        /// Writes the specified signature to the stream.
        /// </summary>
        /// <param name="signature">File signature.</param>
        public void WriteSignature(string signature) => Write(Encoding.GetBytes(signature));

        /// <summary>
        /// Writes a Half to the current position.
        /// </summary>
        public void WriteHalf(Half value) => Write((ushort)value);

        /// <summary>
        /// Writes a Boolean value with Int32 alignment to the stream.
        /// </summary>
        public void WriteBoolean32(bool value) => Write(value ? 1 : 0);

        /// <summary>
        /// Writes an Int24 to the current position.
        /// </summary>
        public void WriteInt24(int value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 16);
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)(value >> 16);
            }

            OutStream.Write(Buffer, 0, 3);
        }

        /// <summary>
        /// Writes a UInt24 to the current position.
        /// </summary>
        public void WriteUInt24(uint value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 16);
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)(value >> 16);
            }

            OutStream.Write(Buffer, 0, 3);
        }

        /// <summary>
        /// Writes a string to the current position.
        /// </summary>
        public override void Write(string value) => Write(Encoding.GetBytes(value));

        /// <summary>
        /// Writes an Int16 to the current position.
        /// </summary>
        public override void Write(short value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 8);
                Buffer[1] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
            }

            OutStream.Write(Buffer, 0, sizeof(short));
        }

        /// <summary>
        /// Writes a UInt16 to the current position.
        /// </summary>
        public override void Write(ushort value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 8);
                Buffer[1] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
            }

            OutStream.Write(Buffer, 0, sizeof(ushort));
        }

        /// <summary>
        /// Writes an Int32 to the current position.
        /// </summary>
        public override void Write(int value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 24);
                Buffer[1] = (byte)(value >> 16);
                Buffer[2] = (byte)(value >> 8);
                Buffer[3] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)(value >> 16);
                Buffer[3] = (byte)(value >> 24);
            }

            OutStream.Write(Buffer, 0, sizeof(int));
        }

        /// <summary>
        /// Writes a UInt32 to the current position.
        /// </summary>
        public override void Write(uint value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 24);
                Buffer[1] = (byte)(value >> 16);
                Buffer[2] = (byte)(value >> 8);
                Buffer[3] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)(value >> 16);
                Buffer[3] = (byte)(value >> 24);
            }

            OutStream.Write(Buffer, 0, sizeof(uint));
        }

        /// <summary>
        /// Writes an Int64 to the current position.
        /// </summary>
        public override void Write(long value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 56);
                Buffer[1] = (byte)(value >> 48);
                Buffer[2] = (byte)(value >> 40);
                Buffer[3] = (byte)(value >> 32);

                Buffer[4] = (byte)(value >> 24);
                Buffer[5] = (byte)(value >> 16);
                Buffer[6] = (byte)(value >> 8);
                Buffer[7] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)(value >> 16);
                Buffer[3] = (byte)(value >> 24);

                Buffer[4] = (byte)(value >> 32);
                Buffer[5] = (byte)(value >> 40);
                Buffer[6] = (byte)(value >> 48);
                Buffer[7] = (byte)(value >> 56);
            }

            OutStream.Write(Buffer, 0, sizeof(long));
        }

        /// <summary>
        /// Writes a UInt64 to the current position.
        /// </summary>
        public override void Write(ulong value)
        {
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(value >> 56);
                Buffer[1] = (byte)(value >> 48);
                Buffer[2] = (byte)(value >> 40);
                Buffer[3] = (byte)(value >> 32);

                Buffer[4] = (byte)(value >> 24);
                Buffer[5] = (byte)(value >> 16);
                Buffer[6] = (byte)(value >> 8);
                Buffer[7] = (byte)value;
            }
            else
            {
                Buffer[0] = (byte)value;
                Buffer[1] = (byte)(value >> 8);
                Buffer[2] = (byte)(value >> 16);
                Buffer[3] = (byte)(value >> 24);

                Buffer[4] = (byte)(value >> 32);
                Buffer[5] = (byte)(value >> 40);
                Buffer[6] = (byte)(value >> 48);
                Buffer[7] = (byte)(value >> 56);
            }

            OutStream.Write(Buffer, 0, sizeof(ulong));
        }

        /// <summary>
        /// Writes a Single to the current position.
        /// </summary>
        public override unsafe void Write(float value) => Write(*(uint*)&value);

        /// <summary>
        /// Writes a Double to the current position.
        /// </summary>
        public override unsafe void Write(double value) => Write(*(ulong*)&value);

        /// <summary>
        /// Writes a Vector2 to the current position.
        /// </summary>
        /// <param name="vect">Vector2 to write.</param>
        public virtual unsafe void Write(Vector2 vect)
        {
            var p = (uint*)&vect.X;
            if (IsBigEndian)
            {
                Buffer[0] = (byte)(*p >> 24);
                Buffer[1] = (byte)(*p >> 16);
                Buffer[2] = (byte)(*p >> 8);
                Buffer[3] = (byte)(*p);

                p = ((uint*)&vect.Y);
                Buffer[4] = (byte)(*p >> 24);
                Buffer[5] = (byte)(*p >> 16);
                Buffer[6] = (byte)(*p >> 8);
                Buffer[7] = (byte)(*p);
            }
            else
            {
                Buffer[0] = (byte)(*p);
                Buffer[1] = (byte)(*p >> 8);
                Buffer[2] = (byte)(*p >> 16);
                Buffer[3] = (byte)(*p >> 24);

                p = ((uint*)&vect.Y);
                Buffer[4] = (byte)(*p);
                Buffer[5] = (byte)(*p >> 8);
                Buffer[6] = (byte)(*p >> 16);
                Buffer[7] = (byte)(*p >> 24);
            }

            Write(Buffer, 0, 8);
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
                Buffer[0] = (byte)(*p >> 24);
                Buffer[1] = (byte)(*p >> 16);
                Buffer[2] = (byte)(*p >> 8);
                Buffer[3] = (byte)(*p);

                p = ((uint*)&vect.Y);
                Buffer[4] = (byte)(*p >> 24);
                Buffer[5] = (byte)(*p >> 16);
                Buffer[6] = (byte)(*p >> 8);
                Buffer[7] = (byte)(*p);

                p = ((uint*)&vect.Z);
                Buffer[8] = (byte)(*p >> 24);
                Buffer[9] = (byte)(*p >> 16);
                Buffer[10] = (byte)(*p >> 8);
                Buffer[11] = (byte)(*p);
            }
            else
            {
                Buffer[0] = (byte)(*p);
                Buffer[1] = (byte)(*p >> 8);
                Buffer[2] = (byte)(*p >> 16);
                Buffer[3] = (byte)(*p >> 24);

                p = ((uint*)&vect.Y);
                Buffer[4] = (byte)(*p);
                Buffer[5] = (byte)(*p >> 8);
                Buffer[6] = (byte)(*p >> 16);
                Buffer[7] = (byte)(*p >> 24);

                p = ((uint*)&vect.Z);
                Buffer[8] = (byte)(*p);
                Buffer[9] = (byte)(*p >> 8);
                Buffer[10] = (byte)(*p >> 16);
                Buffer[11] = (byte)(*p >> 24);
            }

            Write(Buffer, 0, 12);
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
                Buffer[0] = (byte)(*p >> 24);
                Buffer[1] = (byte)(*p >> 16);
                Buffer[2] = (byte)(*p >> 8);
                Buffer[3] = (byte)(*p);

                f = vect.Y;
                Buffer[4] = (byte)(*p >> 24);
                Buffer[5] = (byte)(*p >> 16);
                Buffer[6] = (byte)(*p >> 8);
                Buffer[7] = (byte)(*p);

                f = vect.Z;
                Buffer[8] = (byte)(*p >> 24);
                Buffer[9] = (byte)(*p >> 16);
                Buffer[10] = (byte)(*p >> 8);
                Buffer[11] = (byte)(*p);

                f = vect.W;
                Buffer[12] = (byte)(*p >> 24);
                Buffer[13] = (byte)(*p >> 16);
                Buffer[14] = (byte)(*p >> 8);
                Buffer[15] = (byte)(*p);
            }
            else
            {
                Buffer[0] = (byte)(*p);
                Buffer[1] = (byte)(*p >> 8);
                Buffer[2] = (byte)(*p >> 16);
                Buffer[3] = (byte)(*p >> 24);

                f = vect.Y;
                Buffer[4] = (byte)(*p);
                Buffer[5] = (byte)(*p >> 8);
                Buffer[6] = (byte)(*p >> 16);
                Buffer[7] = (byte)(*p >> 24);

                f = vect.Z;
                Buffer[8] = (byte)(*p);
                Buffer[9] = (byte)(*p >> 8);
                Buffer[10] = (byte)(*p >> 16);
                Buffer[11] = (byte)(*p >> 24);

                f = vect.W;
                Buffer[12] = (byte)(*p);
                Buffer[13] = (byte)(*p >> 8);
                Buffer[14] = (byte)(*p >> 16);
                Buffer[15] = (byte)(*p >> 24);
            }

            Write(Buffer, 0, 16);
        }

        /// <summary>
        /// Writes a Quaternion to the current position.
        /// </summary>
        /// <param name="quat">Quaternion to write.</param>
        public virtual unsafe void Write(Quaternion quat)
        {
            float f = quat.X;
            var p = (uint*)&f;

            if (IsBigEndian)
            {
                Buffer[0] = (byte)(*p >> 24);
                Buffer[1] = (byte)(*p >> 16);
                Buffer[2] = (byte)(*p >> 8);
                Buffer[3] = (byte)(*p);

                f = quat.Y;
                Buffer[4] = (byte)(*p >> 24);
                Buffer[5] = (byte)(*p >> 16);
                Buffer[6] = (byte)(*p >> 8);
                Buffer[7] = (byte)(*p);

                f = quat.Z;
                Buffer[8] = (byte)(*p >> 24);
                Buffer[9] = (byte)(*p >> 16);
                Buffer[10] = (byte)(*p >> 8);
                Buffer[11] = (byte)(*p);

                f = quat.W;
                Buffer[12] = (byte)(*p >> 24);
                Buffer[13] = (byte)(*p >> 16);
                Buffer[14] = (byte)(*p >> 8);
                Buffer[15] = (byte)(*p);
            }
            else
            {
                Buffer[0] = (byte)(*p);
                Buffer[1] = (byte)(*p >> 8);
                Buffer[2] = (byte)(*p >> 16);
                Buffer[3] = (byte)(*p >> 24);

                f = quat.Y;
                Buffer[4] = (byte)(*p);
                Buffer[5] = (byte)(*p >> 8);
                Buffer[6] = (byte)(*p >> 16);
                Buffer[7] = (byte)(*p >> 24);

                f = quat.Z;
                Buffer[8] = (byte)(*p);
                Buffer[9] = (byte)(*p >> 8);
                Buffer[10] = (byte)(*p >> 16);
                Buffer[11] = (byte)(*p >> 24);

                f = quat.W;
                Buffer[12] = (byte)(*p);
                Buffer[13] = (byte)(*p >> 8);
                Buffer[14] = (byte)(*p >> 16);
                Buffer[15] = (byte)(*p >> 24);
            }

            Write(Buffer, 0, 16);
        }
    }
}
