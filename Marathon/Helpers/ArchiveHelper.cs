using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Marathon.IO.Interfaces;

namespace Marathon.Helpers
{
    public static class ArchiveHelper
    {
        private static char[] _directorySeperatorChars = new[] { '/', '\\' };

        public static IArchiveData GetItem(this IArchiveDirectory dir, string path)
        {
            var names = path.Split(_directorySeperatorChars, StringSplitOptions.RemoveEmptyEntries);

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

        public static IArchiveFile GetFile(this IArchiveDirectory dir, string path)
            => dir.GetItem(path) as IArchiveFile;

        public static IArchiveDirectory GetDirectory(this IArchiveDirectory dir, string path)
            => dir.GetItem(path) as IArchiveDirectory;

        public static bool IsFile(this IArchiveData data)
            => data is IArchiveFile;

        public static bool IsDirectory(this IArchiveData data)
            => data is IArchiveDirectory;

        public static bool Remove(this IArchiveFile file)
        {
            if (file.Parent is IArchiveDirectory directory)
                return directory.RemoveFile(file.Name);

            return false;
        }

        public static bool Remove(this IArchiveDirectory dir)
        {
            if (dir.Parent is IArchiveDirectory directory)
                return directory.RemoveDirectory(dir.Name);

            return false;
        }

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
