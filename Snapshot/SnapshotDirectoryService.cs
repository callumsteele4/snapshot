using System.IO;

namespace Snapshot
{
    public class SnapshotDirectoryService : IDirectoryService
    {
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