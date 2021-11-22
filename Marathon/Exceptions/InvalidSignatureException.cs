namespace Marathon.Exceptions
{
    class InvalidSignatureException : Exception
    {
        static readonly new string Message = "The signature read from the stream is incorrect! Expected {0}, got {1}...";

        public InvalidSignatureException(string expectedSignature, string receivedSignature) : base(string.Format(Message, expectedSignature, receivedSignature)) { }
    }
}