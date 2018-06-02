using System.IO;

namespace Snapshot
{
    public class SnapshotFileReader : IFileReader
    {
        public string ReadAllText(CallerMethodInfo callerMethodInfo)
        {
            return File.ReadAllText(callerMethodInfo.SnapshotFilePath);
        }
    }
}