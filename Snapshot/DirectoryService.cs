using System.IO;

namespace Snapshot
{
    public static class DirectoryService
    {
        // TODO: Cover this in tests
        public static string BuildDirectoryPath(string filePath)
        {
            return filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar))
                   + Path.DirectorySeparatorChar
                   + "__snapshots__"
                   + Path.DirectorySeparatorChar
                   + filePath.Substring(
                       filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1,
                       filePath.LastIndexOf('.') - (filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1));
        }

        public static bool Exists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}