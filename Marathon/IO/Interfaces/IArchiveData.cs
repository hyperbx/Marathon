namespace Marathon.IO.Interfaces
{
    public interface IArchiveData
    {
        string Name { get; set; }

        string Path { get; internal set; }

        IArchiveData Parent { get; }

        void Extract(string location);
    }
}
