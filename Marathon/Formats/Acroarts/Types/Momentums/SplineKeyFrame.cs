using Marathon.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class SplineKeyFrame<T> : ISplineKeyFrame where T : unmanaged
    {
        [JsonProperty(Order = -int.MaxValue)]
        public float Frame { get; set; }

        [JsonProperty(Order = -int.MaxValue)]
        public T Data { get; set; }

        public virtual void Read(BinaryObjectReaderEx in_reader)
        {
            Frame = in_reader.Read<float>();
            Data = in_reader.Read<T>();
        }

        public virtual void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Frame);
            in_writer.Write(Data);
        }

        public virtual uint GetParamCount()
        {
            return (uint)(1 + (Marshal.SizeOf<T>() / 4));
        }

        public override string ToString()
        {
            return $"{Frame} - {Data}";
        }
    }
}
