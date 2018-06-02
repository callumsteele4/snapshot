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
        public void WriteAllText(CallerMethodInfo callerMethodInfo, string json)
        {
            var directoryPath = _directoryService.BuildDirectoryPath(callerMethodInfo.FilePath);
            if (!_directoryService.Exists(directoryPath))
            {
                _directoryService.CreateDirectory(directoryPath);
            }
            
            File.WriteAllText(BuildFilePath(callerMethodInfo), json);
        }

        public string ReadAllText(CallerMethodInfo callerMethodInfo)
        {
            return File.ReadAllText(BuildFilePath(callerMethodInfo));
        }

        public bool Exists(CallerMethodInfo callerMethodInfo)
        {
            return File.Exists(BuildFilePath(callerMethodInfo));
        }

        // TODO: Cover this in tests
        // TODO: Add some caching to this
        public string BuildFilePath(CallerMethodInfo callerMethodInfo)
        {
            return _directoryService.BuildDirectoryPath(callerMethodInfo.FilePath)
                   + Path.DirectorySeparatorChar + callerMethodInfo.Name
                   + ".json";
        }
    }
}