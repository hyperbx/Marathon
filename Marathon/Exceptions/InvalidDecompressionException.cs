using System;

namespace Marathon.Exceptions
{
    public class InvalidDecompressionException(long in_expectedLength, long in_receivedLength)
        : Exception($"The uncompressed data is not the correct length. Expected: {in_expectedLength:N0} bytes. Received: {in_receivedLength:N0} bytes.") { }
}
