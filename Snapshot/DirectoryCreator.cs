using System.IO;

namespace Snapshot
{
    public class DirectoryCreator : IDirectoryCreator
    {
        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}