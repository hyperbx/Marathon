using Marathon.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Marathon.IO.Types.FileSystem
{
    public class PhysicalDirectory : IDirectory
    {
        public string Name
        {
            get
            {
                var name = System.IO.Path.GetFileName(Path);

                if (string.IsNullOrEmpty(name))
                    return Path;

                return name;
            }

            set => throw new NotSupportedException();
        }

        public string Path { get; set; }

        public IDirectory Parent
        {
            get => GetParentFromPath(Path);
            set => throw new NotSupportedException();
        }

        public bool IsDirectory => true;

        public INode this[string in_path] => EnumerateNodes(in_path, SearchOption.AllDirectories).FirstOrDefault();

        public INode this[int in_index] => EnumerateNodes().ElementAt(in_index);

        public PhysicalDirectory() { }

        public PhysicalDirectory(string in_path, bool in_createIfNotFound = false)
        {
            Path = System.IO.Path.GetFullPath(in_path);

            if (Directory.Exists(Path))
                return;

            if (in_createIfNotFound)
            {
                Directory.CreateDirectory(Path);
            }
            else
            {
                ThrowHelper.ThrowDirectoryNotFoundException(Path);
            }
        }

        public static PhysicalDirectory CreateDirectoryFromPath(string in_path)
        {
            return new PhysicalDirectory(in_path, true);
        }

        public static PhysicalDirectory GetDirectoryFromPath(string in_path)
        {
            if (string.IsNullOrEmpty(in_path) || !Directory.Exists(in_path))
                return null;

            return new PhysicalDirectory(in_path);
        }

        internal static PhysicalDirectory GetParentFromPath(string in_path)
        {
            if (!System.IO.Path.IsPathRooted(in_path.AsSpan()))
                in_path = in_path.TrimEnd(System.IO.Path.DirectorySeparatorChar);

            return GetDirectoryFromPath(System.IO.Path.GetDirectoryName(in_path));
        }

        public int GetNodeCount(SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.EnumerateFileSystemEntries(Path, "*", in_searchOption).Count();
        }

        public INode[] GetNodes(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return [.. EnumerateNodes(in_searchPattern, in_searchOption)];
        }

        public IEnumerable<INode> EnumerateNodes(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var entry in Directory.EnumerateFileSystemEntries(Path, in_searchPattern, in_searchOption))
            {
                if (FileSystemHelper.GetNodeType(entry) == FileSystemHelper.NodeType.File)
                {
                    yield return new PhysicalFile(entry);
                }
                else
                {
                    yield return new PhysicalDirectory(entry);
                }
            }
        }

        public INode AddNode(INode in_node, bool in_overwrite = true)
        {
            throw new NotImplementedException();
        }

        public IDirectory[] GetDirectories(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return [.. EnumerateDirectories(in_searchPattern, in_searchOption)];
        }

        public IEnumerable<IDirectory> EnumerateDirectories(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var directory in Directory.EnumerateDirectories(Path, in_searchPattern, in_searchOption))
                yield return new PhysicalDirectory(directory);
        }

        public IDirectory GetDirectory()
        {
            return this;
        }

        public IDirectory GetDirectory(string in_path)
        {
            return GetDirectoryFromPath(System.IO.Path.Combine(Path, in_path));
        }

        public IDirectory CreateDirectory(string in_path)
        {
            return CreateDirectoryFromPath(System.IO.Path.Combine(Path, in_path));
        }

        public IDirectory AddDirectory(IDirectory in_directory, bool in_merge = true)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDirectory(string in_path)
        {
            var path = System.IO.Path.Combine(Path, in_path);

            if (!Directory.Exists(path))
                return false;

            try
            {
                Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IFile[] GetFiles(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            return [.. EnumerateFiles(in_searchPattern, in_searchOption)];
        }

        public IEnumerable<IFile> EnumerateFiles(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var file in Directory.EnumerateFiles(Path, in_searchPattern, in_searchOption))
                yield return new PhysicalFile(file);
        }

        public IFile GetFile(string in_path)
        {
            var path = System.IO.Path.Combine(Path, in_path);

            if (!File.Exists(path))
                return null;

            return new PhysicalFile(path);
        }

        public IFile CreateFile(string in_path, bool in_overwrite = true)
        {
            var path = System.IO.Path.Combine(Path, in_path);

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(path);

            ThrowHelper.ThrowDirectoryExistsException(in_path);

            File.WriteAllBytes(path, []);

            return new PhysicalFile(path);
        }

        public IFile AddFile(IFile in_file, bool in_overwrite = true)
        {
            var file = CreateFile(in_file.Name, in_overwrite) as PhysicalFile;

            var destStream = file.Open(FileAccess.Write);
            var srcStream = in_file.Open();

            srcStream.CopyTo(destStream);
            destStream.Dispose();

            return file;
        }

        public bool DeleteFile(string in_path)
        {
            return PhysicalFile.Delete(System.IO.Path.Combine(Path, in_path));
        }

        public IEnumerator<INode> GetEnumerator()
        {
            foreach (string file in Directory.EnumerateFiles(Path))
                yield return new PhysicalFile(file);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object in_obj)
        {
            return in_obj is PhysicalDirectory out_physicalDir && Path == out_physicalDir.Path;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
