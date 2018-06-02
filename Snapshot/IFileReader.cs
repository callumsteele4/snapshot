namespace Snapshot
{
    public interface IFileReader
    {
        string ReadAllText(CallerMethodInfo callerMethodInfo);
    }
}