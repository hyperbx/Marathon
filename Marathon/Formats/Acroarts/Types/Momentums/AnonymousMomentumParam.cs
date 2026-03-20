using Marathon.IO;
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class AnonymousMomentumParam : IBinarySerializableEx
    {
        public uint Data { get; set; }

        [JsonIgnore]
        public uint UInt32 => Data;

        [JsonIgnore]
        public int Int32
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        [JsonIgnore]
        public int Angle => Int32;

        [JsonIgnore]
        public float Float
        {
            get => GetValue<float>();
            set => SetValue(value);
        }

        public AnonymousMomentumParam() { }

        public AnonymousMomentumParam(uint in_data)
        {
            Data = in_data;
        }

        public AnonymousMomentumParam(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Data = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Data);
        }

        public T GetValue<T>() where T : unmanaged
        {
            if (typeof(T) == typeof(uint))
            {
                return (T)(object)Data;
            }
            else if (typeof(T) == typeof(float))
            {
                var buffer = BitConverter.GetBytes(Data);
                return (T)(object)BitConverter.ToSingle(buffer, 0);
            }

            if (Marshal.SizeOf<T>() > 4)
                throw new NotSupportedException();

            try
            {
                return (T)Convert.ChangeType(Data, typeof(T));
            }
            catch
            {
                return default;
            }
        }

        public void SetValue<T>(T in_value) where T : unmanaged
        {
            if (typeof(T) == typeof(uint))
            {
                Data = (uint)(object)in_value;
                return;
            }
            else if (typeof(T) == typeof(float))
            {
                var buffer = BitConverter.GetBytes((float)(object)in_value);
                Data = BitConverter.ToUInt32(buffer);
                return;
            }

            if (Marshal.SizeOf<T>() > 4)
                throw new NotSupportedException();

            try
            {
                Data = (uint)Convert.ChangeType(in_value, typeof(uint));
            }
            catch
            {
                Data = default;
            }
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
