namespace Snapshot
{
    public interface IFileService
    {
        void WriteAllText(string callerName, string callerFilePath, string content);
        string ReadAllText(string callerName, string callerFilePath);
        bool Exists(string callerName, string callerFilePath);
        string BuildFilePath(string callerName, string callerFilePath);
    }
}