using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Marathon.Helpers
{
    public class FilesystemHelper
    {
        public static string ConvertPathToUnix(string in_path)
        {
            return in_path.Replace('\\', '/');
        }

        public static string ConvertPathToWindows(string in_path)
        {
            return in_path.Replace('/', '\\');
        }

        public static string ChangeFileName(string in_filePath, string in_newFileName, bool in_isOriginalExtensions = true)
        {
            if (in_isOriginalExtensions)
                in_newFileName += '.' + string.Join('.', GetAllExtensions(in_filePath));

            return Path.Combine(Path.GetDirectoryName(in_filePath), in_newFileName);
        }

        public static string GetDirectoryNameOfFileName(string in_filePath)
        {
            return Path.Combine(Path.GetDirectoryName(in_filePath), TruncateAllExtensions(in_filePath, true));
        }

        public static string GetRelativeDirectoryName(string in_rootDir, string in_path, bool in_isConvertToUnixSeparators = false)
        {
            var relativePath = in_path[in_rootDir.Length..].TrimStart(Path.DirectorySeparatorChar);

            if (in_isConvertToUnixSeparators)
                relativePath = ConvertPathToUnix(relativePath);

            return relativePath;
        }

        public static List<string> GetAllExtensions(string in_filePath)
        {
            return Path.GetFileName(in_filePath).Split('.', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();
        }

        public static string TruncateAllExtensions(string in_filePath, bool in_isFileNameOnly = false)
        {
            var name = Path.GetFileName(in_filePath).Split('.', StringSplitOptions.RemoveEmptyEntries)[0];

            if (in_isFileNameOnly)
                return name;

            return Path.Combine(Path.GetDirectoryName(in_filePath), name);
        }

        public static string TruncateLastExtension(string in_filePath, bool in_isFileNameOnly = false)
        {
            var name = Path.GetFileNameWithoutExtension(in_filePath);

            if (in_isFileNameOnly)
                return name;

            return Path.Combine(Path.GetDirectoryName(in_filePath), name);
        }

        public static string OmitRootDirectory(string in_path)
        {
            var index = in_path.IndexOf(Path.DirectorySeparatorChar);

            return index >= 0
                ? in_path[(index + 1)..]
                : in_path;
        }

        public static string GetRootDirectory(string in_path)
        {
            var index = in_path.IndexOf(Path.DirectorySeparatorChar);

            return index >= 0
                ? in_path[..(index + 1)]
                : in_path;
        }

        public static FileSystemBasicType GetBasicType(string in_path)
        {
            if (Directory.Exists(in_path))
                return FileSystemBasicType.Directory;

            return FileSystemBasicType.File;
        }

        public enum FileSystemBasicType
        {
            File,
            Directory
        }
    }
}
