using System.IO.Compression;

namespace Marathon.IO.Compression.Events
{
    public class CompressionProgressChangedEventArgs(CompressionMode in_compressionMode, long in_bytesProcessed, long in_bytesTotal)
    {
        /// <summary>
        /// The mode of the current compression operation.
        /// </summary>
        public CompressionMode CompressionMode => in_compressionMode;

        /// <summary>
        /// The amount of bytes processed.
        /// </summary>
        public long BytesProcessed => in_bytesProcessed;

        /// <summary>
        /// The total amount of bytes to process.
        /// </summary>
        public long BytesTotal => in_bytesTotal;

        /// <summary>
        /// The completion progress for this event.
        /// </summary>
        public float Progress => BytesTotal > 0 ? ((float)BytesProcessed / (float)BytesTotal) * 100.0f : 0.0f;

        /// <summary>
        /// Determines whether this event has finished.
        /// </summary>
        public bool IsCompleted => BytesProcessed >= BytesTotal;
    }

    public delegate void CompressionProgressChangedEventHandler(object in_sender, CompressionProgressChangedEventArgs in_args);
}
