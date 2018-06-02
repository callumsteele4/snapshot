namespace Snapshot
{
    public class CallerMethodInfo
    {
        public string Name { get; }
        public string FilePath { get; }
        public string SnapshotFilePath { get; }
        public string SnapshotDirectoryPath { get; }

        public CallerMethodInfo(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
            SnapshotDirectoryPath = PathMapper.BuildDirectoryPath(filePath);
            SnapshotFilePath = PathMapper.BuildFilePath(SnapshotDirectoryPath, name);
        }
    }
}