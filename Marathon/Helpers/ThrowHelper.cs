using Marathon.Exceptions;
using System.IO;

namespace Marathon.Helpers
{
    public class ThrowHelper
    {
        public static void ThrowFileExistsException(string in_path, bool in_checkLocal = true)
        {
            if (in_checkLocal && !File.Exists(in_path))
                return;

            throw new FileExistsException(in_path);
        }

        public static void ThrowDirectoryExistsException(string in_path, bool in_checkLocal = true)
        {
            if (in_checkLocal && !Directory.Exists(in_path))
                return;

            throw new DirectoryExistsException(in_path);
        }

        public static void ThrowFileNotFoundException(string in_path, bool in_checkLocal = true)
        {
            if (in_checkLocal && File.Exists(in_path))
                return;

            throw new FileNotFoundException($"The specified file does not exist: \"{in_path}\"", in_path);
        }

        public static void ThrowDirectoryNotFoundException(string in_path, bool in_checkLocal = true)
        {
            if (in_checkLocal && Directory.Exists(in_path))
                return;

            throw new DirectoryNotFoundException($"The specified directory does not exist: \"{in_path}\"");
        }
    }
}
