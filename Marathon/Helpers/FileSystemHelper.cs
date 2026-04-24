using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marathon.Helpers
{
    public class FileSystemHelper
    {
        public static char[] DirectorySeparators = ['/', '\\', '¥'];

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
            return [.. Path.GetFileName(in_filePath).Split('.', StringSplitOptions.RemoveEmptyEntries).Skip(1)];
        }

        public static List<string> GetExtensions(string in_filePath, int in_count)
        {
            return [.. GetAllExtensions(in_filePath).TakeLast(in_count)];
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

        public static string TruncateExtensions(string in_filePath, int in_count = 1, bool in_isFileNameOnly = false)
        {
            var name = in_filePath;

            if (in_count <= 0)
                return name;

            for (int i = 0; i < in_count; i++)
                name = Path.GetFileNameWithoutExtension(name);

            if (in_isFileNameOnly)
                return name;

            return Path.Combine(Path.GetDirectoryName(in_filePath), name);
        }

        public static string EnsureExtension(string in_path, string in_expectedExtension)
        {
            if (string.IsNullOrEmpty(in_expectedExtension))
                throw new ArgumentNullException(nameof(in_expectedExtension));

            var path = in_path ?? throw new ArgumentNullException(nameof(in_path));
            var expectedExtension = in_expectedExtension.Trim().Trim('.');

            // File name already ends with expected extension.
            if (path.EndsWith(expectedExtension, StringComparison.OrdinalIgnoreCase))
                return path;

            var originalExtensions = GetAllExtensions(path).ToArray();

            // File name has no extension, just append expected extension.
            if (originalExtensions.Length == 0)
                return path + expectedExtension;

            var expectedExtensions = in_expectedExtension.Split('.', StringSplitOptions.RemoveEmptyEntries);
            var max = Math.Min(originalExtensions.Length, expectedExtensions.Length);
            var len = 0;

            // Get number of missing extensions.
            for (int i = max; i >= 1; i--)
            {
                var matches = true;

                for (int j = 0; j < i; j++)
                {
                    var curOriginalExtension = originalExtensions[originalExtensions.Length - i + j];
                    var curExpectedExtension = expectedExtensions[j];

                    if (!string.Equals(curOriginalExtension, curExpectedExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    len = i;
                    break;
                }
            }

            var missingExtensions = expectedExtensions.Skip(len).ToArray();

            if (missingExtensions.Length == 0)
                return path;

            // Append missing extensions.
            return path + '.' + string.Join('.', missingExtensions);
        }

        public static string GetDirectoryTree(IDirectory in_root, string in_name = ".", bool in_showFiles = true, bool in_showSizes = true)
        {
            var result = new StringBuilder();

            void WalkDirectories(IDirectory in_root, string in_indent, bool in_isLast, bool in_isRoot = false)
            {
                if (in_isRoot)
                {
                    result.AppendLine(in_name);
                }
                else
                {
                    result.Append(in_indent);
                    result.Append(in_isLast ? "└───" : "├───");
                    result.AppendLine(in_root.Name);
                }

                var nodes = in_root.GetNodes();
                var maxFileNameLength = 0;
                var hasSubdirs = nodes.Any(x => x.IsDirectory);
                var wasFilePrevious = false;

                foreach (var node in nodes)
                {
                    if (node.IsDirectory)
                        continue;

                    // Compute max file name length for this directory.
                    maxFileNameLength = Math.Max(maxFileNameLength, node.Name.Length);
                }

                // Sort alphanumerically with directories last.
                nodes = [.. nodes.OrderBy(x => x.IsDirectory).ThenBy(x => x.Name, StringComparer.OrdinalIgnoreCase)];

                for (int i = 0; i < nodes.Length; i++)
                {
                    var node = nodes[i];
                    var isLast = i == nodes.Length - 1;
                    var childIndent = in_isRoot ? string.Empty : in_indent + (in_isLast ? "    " : "│   ");

                    if ((in_showFiles && !node.IsDirectory) || wasFilePrevious)
                    {
                        result.Append(childIndent);
                        result.Append(hasSubdirs ? "│   " : "    ");

                        if (node.IsDirectory)
                            result.AppendLine();
                    }

                    if (node.IsDirectory)
                    {
                        WalkDirectories(node as IDirectory, childIndent, isLast);
                    }
                    else if (in_showFiles)
                    {
                        var file = node as IFile;

                        result.Append(file.Name.PadRight(maxFileNameLength));

                        if (in_showSizes)
                        {
                            result.Append("    ");

                            if (file.UncompressedLength != 0)
                            {
                                var compressionRatio = (float)file.Length / (float)file.UncompressedLength;

                                result.Append($"{file.Length:N0} / {file.UncompressedLength:N0} bytes ({compressionRatio:P0})");
                            }
                            else
                            {
                                result.Append($"{file.Length:N0} bytes");
                            }
                        }

                        result.AppendLine();

                        if (isLast)
                            result.AppendLine(childIndent);
                    }

                    if (in_showFiles)
                        wasFilePrevious = !node.IsDirectory;
                }
            }

            WalkDirectories(in_root, "", true, true);

            return result.ToString();
        }

        public static INode WalkPath(IDirectory in_root, string in_path, Func<IDirectory, string, INode, INode> in_onPathNode = null)
        {
            if (string.IsNullOrEmpty(in_path))
                throw new ArgumentNullException(nameof(in_path));

            var segments = in_path.Split(DirectorySeparators, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length <= 0)
                return null;

            var dir = in_root;

            for (int i = 0; i < segments.Length; i++)
            {
                if (dir == null)
                    return null;

                var segment = segments[i];

                switch (segment)
                {
                    case ".":
                        continue;

                    case "..":
                        dir = dir.Parent;
                        continue;

                    default:
                    {
                        var item = dir.FirstOrDefault(x => x.Name == segment);

                        if (in_onPathNode != null)
                        {
                            item = in_onPathNode(dir, segment, item);

                            // Callback returned a file, stop walking.
                            if (item != null && !item.IsDirectory)
                                return item;
                        }

                        // Reached end of path, stop walking.
                        if (i == segments.Length - 1)
                            return item;

                        // Set next directory to walk through.
                        dir = item as IDirectory;

                        break;
                    }
                }
            }

            return dir;
        }

        public static string CreatePath(INode in_node, INode in_top = null)
        {
            var names = new Stack<string>();
            var root = in_node;

            while (root.Parent != in_top)
            {
                names.Push(root.Name);
                root = root.Parent;
            }

            var result = string.Join(Path.DirectorySeparatorChar, names);

            if (in_node.IsDirectory)
                result += Path.DirectorySeparatorChar;

            return result;
        }

        public static string GetTempFileName(string in_path)
        {
            return $".tmp.{Guid.NewGuid()}.{TruncateAllExtensions(in_path, true)}";
        }

        public static void ReplaceFile(string in_srcPath, string in_dstPath, string in_backupPath = null, bool in_overwriteBackup = true)
        {
            if (string.IsNullOrEmpty(in_srcPath))
                throw new ArgumentNullException(nameof(in_srcPath));

            if (string.IsNullOrEmpty(in_dstPath))
                throw new ArgumentNullException(nameof(in_dstPath));

            var srcPathRoot = Path.GetPathRoot(in_srcPath);
            var dstPathRoot = Path.GetPathRoot(in_dstPath);

            if (string.IsNullOrEmpty(srcPathRoot))
                throw new InvalidPathException(in_srcPath);

            if (string.IsNullOrEmpty(dstPathRoot))
                throw new InvalidPathException(in_dstPath);

            if (!string.IsNullOrEmpty(in_backupPath) && !in_overwriteBackup)
                ThrowHelper.ThrowFileExistsException(in_backupPath);

            var srcAttrs = File.GetAttributes(in_srcPath);
            var dstAttrs = File.GetAttributes(in_dstPath);
            var hasReparsePoint = srcAttrs.HasFlag(FileAttributes.ReparsePoint) || dstAttrs.HasFlag(FileAttributes.ReparsePoint);

            // Both files are on the same volume, we can use File.Replace here.
            if (!hasReparsePoint && string.Equals(srcPathRoot, dstPathRoot, StringComparison.OrdinalIgnoreCase))
            {
                File.Replace(in_srcPath, in_dstPath, in_backupPath);
                return;
            }

            if (!string.IsNullOrEmpty(in_backupPath))
                File.Copy(in_dstPath, in_backupPath, in_overwriteBackup);

            using var src = File.OpenRead(in_srcPath);
            using var dst = new FileStream(in_dstPath, FileMode.Create);

            src.CopyTo(dst);
            src.Dispose();

            File.Delete(in_srcPath);
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

        public static FileMode TransformFileAccessToFileMode(FileAccess in_access)
        {
            return in_access switch
            {
                FileAccess.Read => FileMode.Open,
                FileAccess.Write or FileAccess.ReadWrite => FileMode.OpenOrCreate,
                _ => FileMode.OpenOrCreate,
            };
        }

        public static NodeType GetNodeType(string in_path)
        {
            if (Directory.Exists(in_path))
                return NodeType.Directory;

            return NodeType.File;
        }

        public enum NodeType
        {
            File,
            Directory
        }
    }
}
