using System.IO;

namespace Marathon.IO.Helpers
{
    public class StreamHelper
    {
        /// <summary>
        /// Aligns a stream to 32 bytes.
        /// </summary>
        public static void AlignTo32Bytes(Stream stream)
        {
            byte[] zero = new byte[] { 0 };

            for (long pos = stream.Position; pos % 32 != 0; pos++)
                stream.Write(zero, 0, zero.Length);
        }
    }
}
