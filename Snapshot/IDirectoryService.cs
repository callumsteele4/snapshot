namespace Snapshot
{
    public interface IDirectoryService
    {
        bool Exists(string directoryPath);
        void CreateDirectory(string directoryPath);
    }
}