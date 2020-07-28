using System.IO;
using System.Linq;

namespace Marathon.IO.Helpers
{
    class Paths
    {
        /// <summary>
        /// Returns the full path without an extension.
        /// </summary>
        public static string GetPathWithoutExtension(string path)
            => Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));

        /// <summary>
        /// Returns the folder containing the file.
        /// </summary>
        public static string GetContainingFolder(string path)
            => Path.GetFileName(Path.GetDirectoryName(path));

        /// <summary>
        /// Returns if the directory is empty.
        /// </summary>
        public static bool IsDirectoryEmpty(string path)
            => !Directory.EnumerateFileSystemEntries(path).Any();

        /// <summary>
        /// Checks if the path is valid and exists.
        /// </summary>
        public static bool CheckPathLegitimacy(string path)
            => Directory.Exists(path) && path != string.Empty;

        /// <summary>
        /// Checks if the path is valid and exists.
        /// </summary>
        public static bool CheckFileLegitimacy(string path)
            => File.Exists(path) && path != string.Empty;

        /// <summary>
        /// Returns a new path with the specified filename.
        /// </summary>
        public static string ReplaceFilename(string path, string newFile)
            => Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(newFile));
    }
}
