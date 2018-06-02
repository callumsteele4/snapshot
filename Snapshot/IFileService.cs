namespace Snapshot
{
    public interface IFileService
    {
        void WriteAllText(CallerMethodInfo callerMethodInfo, string content);
        string ReadAllText(CallerMethodInfo callerMethodInfo);
        bool Exists(CallerMethodInfo callerMethodInfo);
        string BuildFilePath(CallerMethodInfo callerMethodInfo);
    }
}