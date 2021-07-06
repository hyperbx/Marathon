using System;

namespace Marathon.Exceptions
{
    class InvalidSetParameterType : Exception
    {
        static readonly new string Message = "Got invalid data type {0} in Object Parameter at position {1}...";

        public InvalidSetParameterType(uint invalidType, long position) : base(string.Format(Message, invalidType, position)) { }

        public InvalidSetParameterType(string invalidType, long position) : base(string.Format(Message, invalidType, position)) { }
    }
}