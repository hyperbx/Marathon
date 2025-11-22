using Marathon.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;

namespace Marathon.IO.Types.FileSystem
{
    public class VirtualDirectory : IDirectory
    {
        private List<INode> _nodes = [];

        public string Name { get; set; }

        public string Path
        {
            get => FileSystemHelper.CreatePath(this);
            set => throw new NotSupportedException();
        }

        public IDirectory Parent { get; set; }

        public bool IsDirectory => true;

        public IFile this[string in_name] => GetFile(in_name);

        public INode this[int in_index] => _nodes[in_index];

        public VirtualDirectory() { }

        public VirtualDirectory(string in_name)
        {
            Name = in_name;
        }

        public VirtualDirectory(List<INode> in_nodes)
        {
            _nodes = in_nodes;
        }

        public int GetNodeCount(bool in_isRecursive = true)
        {
            var result = _nodes.Count;

            if (in_isRecursive)
            {
                foreach (var node in _nodes)
                {
                    if (!node.IsDirectory)
                        continue;

                    var dir = node as IDirectory;

                    result += dir.GetNodeCount();
                }
            }

            return result;
        }

        public IEnumerable<INode> GetNodes(string in_searchPattern = "*", bool in_isRecursive = true)
        {
            foreach (var node in _nodes)
            {
                if (in_isRecursive)
                {
                    if (node.IsDirectory)
                    {
                        var dir = node as IDirectory;

                        foreach (var dirNode in dir.GetNodes(in_searchPattern, in_isRecursive))
                        {
                            if (!FileSystemName.MatchesSimpleExpression(in_searchPattern, dirNode.Name))
                                continue;

                            yield return dirNode;
                        }
                    }
                }

                if (!FileSystemName.MatchesSimpleExpression(in_searchPattern, node.Name))
                    continue;

                yield return node;
            }
        }

        public INode AddNode(INode in_node, bool in_overwrite = true)
        {
            if (in_overwrite)
            {
                if (in_node.IsDirectory)
                {
                    DeleteDirectory(in_node.Name);
                }
                else
                {
                    DeleteFile(in_node.Name);
                }
            }
            else if (_nodes.Any(x => x.Name == in_node.Name))
            {
                if (in_node.IsDirectory)
                {
                    ThrowHelper.ThrowDirectoryExistsException(in_node.Path, false);
                }
                else
                {
                    ThrowHelper.ThrowFileExistsException(in_node.Path, false);
                }
            }

            _nodes.Add(in_node);

            return in_node;
        }

        public IEnumerable<IDirectory> GetDirectories(string in_searchPattern = "*")
        {
            foreach (var node in _nodes)
            {
                if (!node.IsDirectory || !FileSystemName.MatchesSimpleExpression(in_searchPattern, node.Name))
                    continue;

                yield return node as IDirectory;
            }
        }

        public IDirectory GetDirectory()
        {
            return this;
        }

        public IDirectory GetDirectory(string in_path)
        {
            var node = FileSystemHelper.WalkPath(this, in_path);

            if (node == null)
                return null;

            if (!node.IsDirectory)
                throw new InvalidDataException("The node at this path is not a directory.");

            return node as IDirectory;
        }

        public IDirectory CreateDirectory(string in_path)
        {
            return (IDirectory)FileSystemHelper.WalkPath(this, in_path, (dir, segment, node) =>
            {
                // A node of this segment exists, keep walking.
                if (node != null)
                    return node;

                // Directory does not exist, start creating directories and walk through them.
                return dir.AddDirectory(new VirtualDirectory(segment) { Parent = dir });
            });
        }

        public IDirectory AddDirectory(IDirectory in_directory, bool in_merge = true)
        {
            if (!in_directory.IsDirectory)
                throw new InvalidDataException($"{nameof(in_directory)} is not a directory.");

            // Ensure we don't add a directory with the same name as a file.
            if (_nodes.Any(x => !x.IsDirectory && x.Name == in_directory.Name))
                ThrowHelper.ThrowFileExistsException(in_directory.Path, false);

            VirtualDirectory dir;

            if (in_directory is VirtualDirectory virtualDir)
            {
                dir = virtualDir;
            }
            else
            {
                dir = new VirtualDirectory(in_directory.Name)
                {
                    Parent = this
                };
            }

            if (!in_merge)
                DeleteDirectory(in_directory.Name);

            // Merge input directory's nodes with this directory.
            foreach (var node in GetNodes(in_isRecursive: false))
            {
                if (!node.IsDirectory || node.Name != in_directory.Name)
                    continue;

                var dstNode = node as IDirectory;
                var srcNodes = in_directory.GetNodes(in_isRecursive: false);

                for (int i = 0; i < srcNodes.Count(); i++)
                {
                    var srcNode = srcNodes.ElementAt(i);

                    if (srcNode.IsDirectory)
                    {
                        dstNode.AddDirectory(srcNode as IDirectory);
                    }
                    else
                    {
                        dstNode.AddFile(srcNode as IFile);
                    }
                }

                return dstNode;
            }

            // Add remaining nodes to new directory.
            {
                var srcNodes = in_directory.GetNodes(in_isRecursive: false);

                for (int i = 0; i < srcNodes.Count(); i++)
                {
                    var srcNode = srcNodes.ElementAt(i);

                    if (srcNode.IsDirectory)
                    {
                        dir.AddDirectory(srcNode as IDirectory);
                    }
                    else
                    {
                        dir.AddFile(srcNode as IFile);
                    }
                }
            }

            _nodes.Add(dir);

            return dir;
        }

        public bool DeleteDirectory(string in_path)
        {
            return _nodes.Remove(GetDirectory(in_path));
        }

        public IEnumerable<IFile> GetFiles(string in_searchPattern = "*")
        {
            foreach (var node in _nodes)
            {
                if (node.IsDirectory || !FileSystemName.MatchesSimpleExpression(in_searchPattern, node.Name))
                    continue;

                yield return node as IFile;
            }
        }

        public IFile GetFile(string in_path)
        {
            var node = FileSystemHelper.WalkPath(this, in_path);

            if (node == null)
                return null;

            if (node.IsDirectory)
                throw new InvalidDataException("The node at this path is not a file.");

            return node as IFile;
        }

        public IFile CreateFile(string in_path, bool in_overwrite = true)
        {
            var parentPath = System.IO.Path.GetDirectoryName(in_path);

            if (!string.IsNullOrEmpty(parentPath) && GetDirectory(parentPath) == null)
                ThrowHelper.ThrowDirectoryNotFoundException(parentPath, false);

            return (IFile)FileSystemHelper.WalkPath(this, in_path, (dir, segment, node) =>
            {
                // This node is a directory, keep walking.
                if (node != null && node.IsDirectory)
                    return node;

                // This node is a file.
                if (node != null && !node.IsDirectory)
                {
                    if (in_overwrite)
                    {
                        dir.DeleteFile(segment);
                    }
                    else
                    {
                        ThrowHelper.ThrowFileExistsException(in_path, false);
                    }
                }

                // Create new file, stop walking.
                return dir.AddNode(new VirtualFile(segment) { Parent = dir });
            });
        }

        public IFile AddFile(IFile in_file, bool in_overwrite = true)
        {
            if (in_file.IsDirectory)
                throw new InvalidDataException($"{nameof(in_file)} is not a file.");

            if (in_file is VirtualFile virtualFile)
                return AddNode(virtualFile, in_overwrite) as IFile;

            var file = CreateFile(in_file.Name, in_overwrite) as VirtualFile;

            file.Length = in_file.Length;
            file.UncompressedLength = in_file.UncompressedLength;
            file.BaseStream = in_file.Open();

            return file;
        }

        public bool DeleteFile(string in_path)
        {
            return _nodes.Remove(GetFile(in_path));
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return base.ToString();

            return Name;
        }
    }
}
