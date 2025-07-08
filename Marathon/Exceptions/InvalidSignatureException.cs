using System;

namespace Marathon.Exceptions
{
    public class InvalidSignatureException : Exception
    {
        private static string GetExceptionMessage(object in_expectedSig, object in_receivedSig)
        {
            var msg = "Signature mismatch ";

            if (in_expectedSig.GetType() == typeof(string))
            {
                msg += $"(expected: {in_expectedSig}, received: {in_receivedSig})";
            }
            else
            {
                msg += $"(expected: 0x{in_expectedSig:X}, received: 0x{in_receivedSig:X})";
            }

            return msg;
        }

        public InvalidSignatureException(object in_expectedSig, object in_receivedSig) : base(GetExceptionMessage(in_expectedSig, in_receivedSig)) { }
    }
}
