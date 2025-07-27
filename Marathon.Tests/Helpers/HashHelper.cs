using System.IO.Hashing;

namespace Marathon.Tests.Helpers
{
    internal class HashHelper
    {
        public static ulong ComputeStreamXxHash3(Stream in_stream)
        {
            var result = new XxHash3();
            
            result.Append(in_stream);

            return result.GetCurrentHashAsUInt64();
        }

        public static ulong ComputeFileXxHash3(string in_path)
        {
            ulong result;

            using (var fs = File.OpenRead(in_path))
                result = ComputeStreamXxHash3(fs);

            return result;
        }

        public static ulong ComputeBufferXxHash3(byte[] in_buffer)
        {
            var result = new XxHash3();

            result.Append(in_buffer);

            return result.GetCurrentHashAsUInt64();
        }
    }
}
