using System.IO;

namespace Snapshot
{
    public static class FileService
    {
        // TODO: Test that directory is created when needed here.
        public static void WriteAllText(string callerName, string callerFilePath, string json)
        {
            var directoryPath = DirectoryService.BuildDirectoryPath(callerFilePath);
            if (!DirectoryService.Exists(directoryPath))
            {
                DirectoryService.CreateDirectory(directoryPath);
            }
            
            File.WriteAllText(BuildFilePath(callerName, callerFilePath), json);
        }

        public static string ReadAllText(string callerName, string callerFilePath)
        {
            return File.ReadAllText(BuildFilePath(callerName, callerFilePath));
        }

        public static bool Exists(string callerName, string callerFilePath)
        {
            return File.Exists(BuildFilePath(callerName, callerFilePath));
        }

        // TODO: Cover this in tests
        // TODO: Add some caching to this
        public static string BuildFilePath(string callerName, string callerFilePath)
        {
            return DirectoryService.BuildDirectoryPath(callerFilePath)
                   + Path.DirectorySeparatorChar + callerName
                   + ".json";
        }
    }
}