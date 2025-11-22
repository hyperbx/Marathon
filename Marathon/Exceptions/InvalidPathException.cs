using System;

namespace Marathon.Exceptions
{
    public class InvalidPathException(string in_path) : Exception($"The specified path is invalid: {in_path}") { }
}
