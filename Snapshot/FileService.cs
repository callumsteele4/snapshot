using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Snapshot
{
    public static class FileService
    {
        // TODO: This probably doesn't belong here.
        public static (string name, string filePath) GetTestMethodNameAndFilePath()
        {
            var testFrame = new StackTrace(true).GetFrames()
               .Single(frame => frame.GetMethod().GetCustomAttributes<FactAttribute>(true).Any());
            return (testFrame.GetMethod().Name, testFrame.GetFileName());
        }

        public static void WriteAllText(string name, string filePath, string json)
        {
            var directoryPath = DirectoryService.BuildDirectoryPath(filePath);
            if (!DirectoryService.Exists(directoryPath))
            {
                DirectoryService.CreateDirectory(directoryPath);
            }
            
            File.WriteAllText(BuildFilePath(name, filePath), json);
        }

        public static string ReadAllText(string name, string filePath)
        {
            return File.ReadAllText(BuildFilePath(name, filePath));
        }

        public static bool Exists(string name, string filePath)
        {
            return File.Exists(BuildFilePath(name, filePath));
        }

        // TODO: Add some caching to this
        public static string BuildFilePath(string name, string filePath)
        {
            return DirectoryService.BuildDirectoryPath(filePath)
                   + Path.DirectorySeparatorChar + name
                   + ".json";
        }
    }
}