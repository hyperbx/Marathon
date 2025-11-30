using Amicitia.IO.Binary;
using System;
using System.Text;

namespace Marathon.IO.Types
{
    public struct FourCC(Endianness in_endianness = Endianness.Big) : IBinarySerializable
    {
        public uint Data;

        public Endianness Endianness { get; private set; } = in_endianness;

        public FourCC(uint in_data, Endianness in_endianness = Endianness.Big) : this(in_endianness)
        {
            Data = in_data;
        }

        public FourCC(int in_data, Endianness in_endianness = Endianness.Big) : this(in_endianness)
        {
            Data = (uint)in_data;
        }

        public FourCC(string in_signature, Endianness in_endianness = Endianness.Big) : this(in_endianness)
        {
            if (in_signature.Length > 4)
                throw new ArgumentException("The provided signature is longer than four characters.");

            Data = BitConverter.ToUInt32(Encoding.UTF8.GetBytes(in_signature));
        }

        public void Read(BinaryObjectReader in_reader)
        {
            Data = in_reader.Read<uint>();
            Endianness = in_reader.Endianness;
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            var oldEndianness = in_writer.Endianness;
            in_writer.Endianness = Endianness;
            in_writer.Write(Data);
            in_writer.Endianness = oldEndianness;
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is string out_str)
            {
                return ToString() == out_str;
            }
            else if (in_obj is uint out_uint)
            {
                return Data == out_uint;
            }
            else if (in_obj is int out_int)
            {
                return Data == out_int;
            }

            return false;
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(BitConverter.GetBytes(Data));
        }
    }
}
