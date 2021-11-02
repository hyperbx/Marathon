using System;
using System.Collections.Generic;
using System.Linq;
using Marathon.IO.Interfaces;

namespace Marathon.Helpers
{
    public static class ArchiveHelper
    {
        public static char[] DirectorySeperators = new[] { '/', '\\' };

        /// <summary>
        /// Retrieves an item from the input directory.
        /// </summary>
        /// <param name="dir">Directory containing the item.</param>
        /// <param name="path">Path to the item (can include subdirectories).</param>
        public static IArchiveData GetItem(this IArchiveDirectory dir, string path)
        {
            var names = path.Split(DirectorySeperators, StringSplitOptions.RemoveEmptyEntries);

            // Return null if path split didn't get any results.
            if (names.Length == 0)
                return null;

            IArchiveDirectory current = dir;

            for (int i = 0; i < names.Length; i++)
            {
                var name = names[i];

                if (current == null)
                    return null;

                switch (name)
                {
                    case ".":
                        continue;

                    case "..":
                        current = current.Parent as IArchiveDirectory;
                        continue;

                    default:
                    {
                        var item = current.FirstOrDefault(x => x.Name == name);

                        if (i == names.Length - 1)
                            return item;

                        current = item as IArchiveDirectory;

                        break;
                    }
                }
            }
            
            return current;
        }

        /// <summary>
        /// Retrieves a file from the input directory.
        /// </summary>
        /// <param name="dir">Directory containing the file.</param>
        /// <param name="path">Path to the file (can include subdirectories).</param>
        public static IArchiveFile GetFile(this IArchiveDirectory dir, string path)
            => dir.GetItem(path) as IArchiveFile;

        /// <summary>
        /// Retrieves a subdirectory from the input directory.
        /// </summary>
        /// <param name="dir">Directory containing the subdirectory.</param>
        /// <param name="path">Path to the subdirectory (can include subdirectories).</param>
        public static IArchiveDirectory GetDirectory(this IArchiveDirectory dir, string path)
            => dir.GetItem(path) as IArchiveDirectory;

        /// <summary>
        /// Verifies whether or not the input data is a file.
        /// </summary>
        /// <param name="data">Data to verify.</param>
        public static bool IsFile(this IArchiveData data)
            => data is IArchiveFile;

        /// <summary>
        /// Verifies whether or not the input data is a directory.
        /// </summary>
        /// <param name="data">Data to verify.</param>
        public static bool IsDirectory(this IArchiveData data)
            => data is IArchiveDirectory;

        /// <summary>
        /// Removes the current file.
        /// </summary>
        /// <param name="file">File to remove.</param>
        public static bool Remove(this IArchiveFile file)
        {
            if (file.Parent is IArchiveDirectory directory)
                return directory.RemoveFile(file.Name);

            return false;
        }

        /// <summary>
        /// Removes the current directory.
        /// </summary>
        /// <param name="dir">Directory to remove.</param>
        public static bool Remove(this IArchiveDirectory dir)
        {
            if (dir.Parent is IArchiveDirectory directory)
                return directory.RemoveDirectory(dir.Name);

            return false;
        }

        /// <summary>
        /// Merges the contents of another directory with another.
        /// </summary>
        /// <param name="to">Destination directory.</param>
        /// <param name="from">Directory to merge data from.</param>
        public static void MergeWith(this IArchiveDirectory to, IArchiveDirectory from)
        {
            foreach (var item in from)
            {
                if (item is IArchiveDirectory dir)
                {
                    var selfDir = to.GetDirectory(item.BuildPath(from.Parent));

                    if (selfDir == null)
                    {
                        to.Add(item, true);
                        continue;
                    }

                    selfDir.MergeWith(dir);
                }
                else
                {
                    to.Add(item, true);
                }
            }
        }

        /// <summary>
        /// Builds a path to the input data.
        /// </summary>
        /// <param name="data">Data to build a path for.</param>
        /// <param name="top">Highest directory in the stack to build the path to.</param>
        public static string BuildPath(this IArchiveData data, IArchiveData top = null)
        {
            var names = new Stack<string>(4);
            var current = data;

            while (current.Parent != top)
            {
                names.Push(current.Name);
                current = current.Parent;
            }

            string path = string.Join("/", names);
            return data.IsFile() ? path : $"{path}/";
        }

        /// <summary>
        /// Gets the total number of items in the directory.
        /// </summary>
        public static int GetTotalCount(this IArchiveDirectory self)
        {
            int totalCount = self.Count();

            foreach (var item in self)
            {
                if (item is IArchiveDirectory dir)
                    totalCount += dir.GetTotalCount();
            }

            return totalCount;
        }
    }
}
