using System.IO;

namespace Snapshot
{
    public static class DirectoryService
    {
        // TODO: Cover this in tests
        public static string BuildDirectoryPath(string callerFilePath)
        {
            return callerFilePath.Substring(0, callerFilePath.LastIndexOf(Path.DirectorySeparatorChar))
                   + Path.DirectorySeparatorChar
                   + "__snapshots__"
                   + Path.DirectorySeparatorChar
                   + callerFilePath.Substring(
                       callerFilePath.LastIndexOf(Path.DirectorySeparatorChar) + 1,
                       callerFilePath.LastIndexOf('.') - (callerFilePath.LastIndexOf(Path.DirectorySeparatorChar) + 1));
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