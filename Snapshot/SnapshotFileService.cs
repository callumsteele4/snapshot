using System.IO;

namespace Snapshot
{
    public class SnapshotFileService : IFileService
    {
        private static IDirectoryService _directoryService;

        public SnapshotFileService(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }
        
        // TODO: Test that directory is created when needed here.
        public void WriteAllText(string callerName, string callerFilePath, string json)
        {
            var directoryPath = _directoryService.BuildDirectoryPath(callerFilePath);
            if (!_directoryService.Exists(directoryPath))
            {
                _directoryService.CreateDirectory(directoryPath);
            }
            
            File.WriteAllText(BuildFilePath(callerName, callerFilePath), json);
        }

        public string ReadAllText(string callerName, string callerFilePath)
        {
            return File.ReadAllText(BuildFilePath(callerName, callerFilePath));
        }

        public bool Exists(string callerName, string callerFilePath)
        {
            return File.Exists(BuildFilePath(callerName, callerFilePath));
        }

        // TODO: Cover this in tests
        // TODO: Add some caching to this
        public string BuildFilePath(string callerName, string callerFilePath)
        {
            return _directoryService.BuildDirectoryPath(callerFilePath)
                   + Path.DirectorySeparatorChar + callerName
                   + ".json";
        }
    }
}