using System.IO;

namespace Snapshot
{
    public class SnapshotDirectoryService : IDirectoryService
    {
        // TODO: Cover this in tests
        public string BuildDirectoryPath(string callerFilePath)
        {
            return callerFilePath.Substring(0, callerFilePath.LastIndexOf(Path.DirectorySeparatorChar))
                   + Path.DirectorySeparatorChar
                   + "__snapshots__"
                   + Path.DirectorySeparatorChar
                   + callerFilePath.Substring(
                       callerFilePath.LastIndexOf(Path.DirectorySeparatorChar) + 1,
                       callerFilePath.LastIndexOf('.') - (callerFilePath.LastIndexOf(Path.DirectorySeparatorChar) + 1));
        }

        public bool Exists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}