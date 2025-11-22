using System;

namespace Marathon.Exceptions
{
    public class DirectoryExistsException(string in_path) : Exception($"The specified directory already exists: {in_path}") { }
}
