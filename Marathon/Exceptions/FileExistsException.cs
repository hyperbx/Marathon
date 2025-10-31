using System;

namespace Marathon.Exceptions
{
    public class FileExistsException(string in_path) : Exception($"The specified file already exists: {in_path}") { }
}
