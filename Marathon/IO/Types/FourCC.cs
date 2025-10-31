using System;
using System.Text;

namespace Marathon.IO.Types
{
    public struct FourCC
    {
        public uint Data;

        public FourCC() { }

        public FourCC(uint in_data)
        {
            Data = in_data;
        }

        public FourCC(int in_data)
        {
            Data = (uint)in_data;
        }

        public FourCC(string in_signature)
        {
            if (in_signature.Length > 4)
                throw new ArgumentException("The provided signature is longer than four characters.");

            Data = BitConverter.ToUInt32(Encoding.UTF8.GetBytes(in_signature));
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
            var bytes = BitConverter.GetBytes(Data);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
