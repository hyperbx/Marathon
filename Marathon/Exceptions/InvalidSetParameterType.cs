using System;

namespace Marathon.Exceptions
{
    public class InvalidSetParameterType : Exception
    {
        static readonly new string Message = "Invalid data type {0} in object parameter at position {1}...";

        public InvalidSetParameterType(uint in_invalidType, long in_pos) : base(string.Format(Message, in_invalidType, in_pos)) { }

        public InvalidSetParameterType(string in_invalidType, long in_pos) : base(string.Format(Message, in_invalidType, in_pos)) { }
    }
}