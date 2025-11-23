namespace Marathon.IO.Types.FileSystem
{
    public interface INode
    {
        /// <summary>
        /// The name of this node.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The path to this node.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// The directory containing this node.
        /// </summary>
        IDirectory Parent { get; set; }

        /// <summary>
        /// Determines whether this node is a directory.
        /// </summary>
        bool IsDirectory { get; }
    }
}
