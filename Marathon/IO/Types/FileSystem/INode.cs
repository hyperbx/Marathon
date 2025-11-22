namespace Marathon.IO.Types.FileSystem
{
    public interface INode
    {
        string Name { get; set; }

        string Path { get; set; }

        IDirectory Parent { get; set; }

        bool IsDirectory { get; }
    }
}
