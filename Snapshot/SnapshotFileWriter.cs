using System.IO;

namespace Snapshot
{
    public class SnapshotFileWriter : IFileWriter
    {
        public void WriteAllText(CallerMethodInfo callerMethodInfo, string json)
        {
            File.WriteAllText(callerMethodInfo.SnapshotFilePath, json);
        }
    }
}