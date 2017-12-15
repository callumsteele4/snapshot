namespace Snapshot
{
    public interface IDirectoryService
    {
        string BuildDirectoryPath(string callerFilePath);
        bool Exists(string directoryPath);
        void CreateDirectory(string directoryPath);
    }
}