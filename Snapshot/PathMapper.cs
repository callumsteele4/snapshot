using System.IO;

namespace Snapshot
{
    public static class PathMapper
    {
        // TODO: Cover this in tests
        // TODO: Add some caching to this
        public static string BuildFilePath(string snapshotDirectoryPath, string name)
        {
            return snapshotDirectoryPath
                   + Path.DirectorySeparatorChar + name
                   + ".json";
        }
        
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
    }
}