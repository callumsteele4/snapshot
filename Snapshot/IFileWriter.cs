namespace Snapshot
{
    public interface IFileWriter
    {
        void WriteAllText(CallerMethodInfo callerMethodInfo, string content);
    }
}