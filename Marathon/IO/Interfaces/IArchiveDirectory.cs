namespace Marathon.IO.Interfaces
{
    public interface IArchiveDirectory : IArchiveData, IEnumerable<IArchiveData>
    {
        void Add(IArchiveData data, bool overwrite = false);

        bool RemoveFile(string name);

        bool RemoveDirectory(string name);

        bool FileExists(string name);

        bool DirectoryExists(string name);
    }
}
