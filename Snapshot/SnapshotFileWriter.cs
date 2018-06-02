using System.IO;

namespace Snapshot
{
    public class SnapshotFileWriter : IFileWriter
    {
        private static IDirectoryService _directoryService;

        public SnapshotFileWriter(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }
        
        public void WriteAllText(CallerMethodInfo callerMethodInfo, string json)
        {
            if (!_directoryService.Exists(callerMethodInfo.SnapshotDirectoryPath))
            {
                _directoryService.CreateDirectory(callerMethodInfo.SnapshotDirectoryPath);
            }
            
            File.WriteAllText(callerMethodInfo.SnapshotFilePath, json);
        }
    }
}