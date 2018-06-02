namespace Snapshot
{
    public interface IFileExistenceChecker
    {
        bool Exists(CallerMethodInfo callerMethodInfo);
    }
}