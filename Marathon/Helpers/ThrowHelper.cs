using Marathon.Exceptions;
using System.IO;

namespace Marathon.Helpers
{
    public class ThrowHelper
    {
        public static void ThrowFileExistsException(string in_path)
        {
            if (!File.Exists(in_path))
                return;
            
            throw new FileExistsException(in_path);
        }

        public static void ThrowFileNotFoundException(string in_path)
        {
            if (File.Exists(in_path))
                return;

            throw new FileNotFoundException("The specified file does not exist.", in_path);
        }
    }
}
