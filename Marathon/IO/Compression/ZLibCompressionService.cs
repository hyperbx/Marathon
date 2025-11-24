using Marathon.Exceptions;
using Marathon.IO.Compression.Events;
using System.IO;
using System.IO.Compression;

namespace Marathon.IO.Compression
{
    public class ZLibCompressionService : ICompressionService
    {
        public event CompressionProgressChangedEventHandler ProgressChanged;

        public long Compress(Stream in_srcStream, Stream in_destStream, CompressionLevel in_compressionLevel = CompressionLevel.Optimal)
        {
            if (in_srcStream.Length <= 0)
                return 0;

            if (in_srcStream.CanSeek)
                in_srcStream.Position = 0;

            var bytesProcessed = 0L;

            using (var zlibStream = new ZLibStream(in_destStream, in_compressionLevel, true))
            {
                var buffer = new byte[81920];
                var bufferRead = 0;

                while ((bufferRead = in_srcStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    zlibStream.Write(buffer, 0, bufferRead);

                    bytesProcessed += bufferRead;

                    if (ProgressChanged != null)
                        OnCompress(new CompressionProgressChangedEventArgs(CompressionMode.Compress, in_srcStream.Position, in_srcStream.Length));
                }
            }

            return bytesProcessed;
        }

        public long Decompress(Stream in_srcStream, Stream in_destStream, long in_uncompressedLength = -1)
        {
            if (in_srcStream.Length <= 0 || in_uncompressedLength == 0)
                return 0;

            if (in_srcStream.CanSeek)
                in_srcStream.Position = 0;

            var bytesProcessed = 0L;

            using (var zlibStream = new ZLibStream(in_srcStream, CompressionMode.Decompress, true))
            {
                var buffer = new byte[81920];
                var bufferRead = 0;

                while ((bufferRead = zlibStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    in_destStream.Write(buffer, 0, bufferRead);

                    bytesProcessed += bufferRead;

                    if (ProgressChanged != null)
                    {
                        if (in_uncompressedLength < 0)
                        {
                            OnDecompress(new CompressionProgressChangedEventArgs(CompressionMode.Decompress, in_srcStream.Position, in_srcStream.Length));
                        }
                        else
                        {
                            OnDecompress(new CompressionProgressChangedEventArgs(CompressionMode.Decompress, bytesProcessed, in_uncompressedLength));
                        }
                    }
                }
            }

            if (in_uncompressedLength > 0 && bytesProcessed != in_uncompressedLength)
                throw new InvalidDecompressionException(in_uncompressedLength, bytesProcessed);

            return bytesProcessed;
        }

        public void OnCompress(CompressionProgressChangedEventArgs in_args)
        {
            ProgressChanged?.Invoke(this, in_args);
        }

        public void OnDecompress(CompressionProgressChangedEventArgs in_args)
        {
            ProgressChanged?.Invoke(this, in_args);
        }
    }
}
