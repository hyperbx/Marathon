using System.Collections.Generic;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Marathon.IO.Types.FileSystem
{
    public interface IDirectory : INode, IEnumerable<INode>
    {
        bool IsDirectory => true;

        IFile this[string in_name] { get; }

        INode this[int in_index] { get; }

        int GetNodeCount(bool in_isRecursive = true);

        IEnumerable<INode> GetNodes(string in_searchPattern = "*", bool in_isRecursive = true);

        INode AddNode(INode in_node, bool in_overwrite = true);

        IEnumerable<IDirectory> GetDirectories(string in_searchPattern = "*");

        IDirectory GetDirectory();

        IDirectory GetDirectory(string in_path);

        IDirectory CreateDirectory(string in_path);

        IDirectory AddDirectory(IDirectory in_directory, bool in_merge = true);

        bool DeleteDirectory(string in_path);

        IEnumerable<IFile> GetFiles(string in_searchPattern = "*");

        IFile GetFile(string in_path);

        IFile CreateFile(string in_path, bool in_overwrite = true);

        IFile AddFile(IFile in_file, bool in_overwrite = true);

        bool DeleteFile(string in_path);
    }
}

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
