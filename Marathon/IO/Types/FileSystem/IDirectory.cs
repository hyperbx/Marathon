using System.Collections.Generic;
using System.IO;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Marathon.IO.Types.FileSystem
{
    public interface IDirectory : INode, IEnumerable<INode>
    {
        /// <summary>
        /// This node is a directory.
        /// </summary>
        bool IsDirectory => true;

        INode this[string in_path] { get; }

        INode this[int in_index] { get; }

        /// <summary>
        /// Gets the total number of nodes in this directory.
        /// </summary>
        /// <param name="in_isRecursive">Determines whether to include nodes from subdirectories in the total.</param>
        /// <returns>The total number of nodes in this directory.</returns>
        int GetNodeCount(SearchOption in_searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Returns the nodes that match the specified search pattern in this directory, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of nodes in this directory.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An array of nodes in this directory that match the specified criteria, or an empty array if no nodes are found.
        /// </returns>
        INode[] GetNodes(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Returns an enumerable collection of nodes that match a search pattern in this directory, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of nodes in this directory.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An enumerable collection of nodes in this directory that match the specified search pattern and search option.
        /// </returns>
        IEnumerable<INode> EnumerateNodes(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Adds a node to this directory.
        /// </summary>
        /// <param name="in_node">The node to add.</param>
        /// <param name="in_overwrite">Determines whether to overwrite an existing node in this directory of the same name.</param>
        /// <returns>The node added to this directory.</returns>
        INode AddNode(INode in_node, bool in_overwrite = true);

        /// <summary>
        /// Returns the directories that match the specified search pattern in this directory, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of directories in this directory.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An array of directories in this directory that match the specified criteria, or an empty array if no directories are found.
        /// </returns>
        IDirectory[] GetDirectories(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Returns an enumerable collection of directories that match a search pattern in this directory, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of directories in this directory.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An enumerable collection of directories in this directory that match the specified search pattern and search option.
        /// </returns>
        IEnumerable<IDirectory> EnumerateDirectories(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Returns the current directory.
        /// </summary>
        /// <returns>The current directory.</returns>
        IDirectory GetDirectory();

        /// <summary>
        /// Returns the directory at the specified path.
        /// </summary>
        /// <param name="in_path">The path to the directory to get.</param>
        /// <returns>The directory at the specified path.</returns>
        IDirectory GetDirectory(string in_path);

        /// <summary>
        /// Creates a directory at the specified path.
        /// </summary>
        /// <param name="in_path">The path to create the directory at.</param>
        /// <returns>The new directory at the specified path.</returns>
        IDirectory CreateDirectory(string in_path);

        /// <summary>
        /// Adds a directory to this directory.
        /// </summary>
        /// <param name="in_directory">The directory to add.</param>
        /// <param name="in_merge">Determines whether to merge this directory with another directory of the same name.</param>
        /// <returns>The directory added to this directory.</returns>
        IDirectory AddDirectory(IDirectory in_directory, bool in_merge = true);

        /// <summary>
        /// Deletes a directory at the specified path.
        /// </summary>
        /// <param name="in_path">The path to the directory to delete.</param>
        /// <returns><b>true</b> if the deletion was successful. Otherwise, <b>false</b>.</returns>
        bool DeleteDirectory(string in_path);

        /// <summary>
        /// Returns the files that match the specified search pattern in this directory, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of files in this directory.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An array of files in this directory that match the specified criteria, or an empty array if no files are found.
        /// </returns>
        IFile[] GetFiles(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Returns an enumerable collection of files that match a search pattern in this directory, and optionally searches subdirectories.
        /// </summary>
        /// <param name="in_searchPattern">
        ///     The search string to match against the names of files in this directory.
        ///     This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.
        /// </param>
        /// <param name="in_searchOption">
        ///     One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.
        ///     The default value is <see cref="SearchOption.TopDirectoryOnly"/>.
        /// </param>
        /// <returns>
        ///     An enumerable collection of files in this directory that match the specified search pattern and search option.
        /// </returns>
        IEnumerable<IFile> EnumerateFiles(string in_searchPattern = "*", SearchOption in_searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Returns the file at the specified path.
        /// </summary>
        /// <param name="in_path">The path to the file to get.</param>
        /// <returns>The file at the specified path.</returns>
        IFile GetFile(string in_path);

        /// <summary>
        /// Creates a file at the specified path.
        /// </summary>
        /// <param name="in_path">The path to create the file at.</param>
        /// <param name="in_overwrite">Determines whether to overwrite an existing file in this directory of the same name.</param>
        /// <returns>The new file at the specified path.</returns>
        IFile CreateFile(string in_path, bool in_overwrite = true);

        /// <summary>
        /// Adds a file to this directory.
        /// </summary>
        /// <param name="in_file">The file to add.</param>
        /// <param name="in_overwrite">Determines whether to overwrite an existing file in this directory of the same name.</param>
        /// <returns>The file added to this directory.</returns>
        IFile AddFile(IFile in_file, bool in_overwrite = true);

        /// <summary>
        /// Deletes a file at the specified path.
        /// </summary>
        /// <param name="in_path">The path to the file to delete.</param>
        /// <returns><b>true</b> if the deletion was successful. Otherwise, <b>false</b>.</returns>
        bool DeleteFile(string in_path);
    }
}

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
