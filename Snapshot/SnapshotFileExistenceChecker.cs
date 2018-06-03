using System.IO;

namespace Snapshot
{
    public class SnapshotFileExistenceChecker : IFileExistenceChecker
    {
        public bool Exists(CallerMethodInfo callerMethodInfo)
        {
            return File.Exists(callerMethodInfo.SnapshotFilePath);
        }
    }
}