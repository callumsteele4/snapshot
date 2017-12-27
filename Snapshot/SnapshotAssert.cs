using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Sdk;

namespace Snapshot
{
    // TODO: Work out how to make this part of the Assert partial class and testable as such
    public class SnapshotAssert
    {
        private readonly IFileService _fileService;

        public SnapshotAssert()
        {
            _fileService = new SnapshotFileService(new SnapshotDirectoryService());
        }

        internal SnapshotAssert(IFileService fileService)
        {
            _fileService = fileService;
        }

        // TODO: Callername will only work for when directly called by the test method,
        // need to look at how to get name of the method in the stack trace which has the [Fact] attribute.
        // Work out how to access caller name and caller file path using reflection.
        public void Snapshot<T>(
            T input,
            bool overwriteExistingSnapshot = false,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            var json = JsonConvert.SerializeObject(input);

            if (_fileService.Exists(callerName, callerFilePath))
            {
                var snapshotJson = _fileService.ReadAllText(callerName, callerFilePath);

                try
                {
                    Assert.Equal(snapshotJson, json);
                }
                catch (EqualException exception)
                {
                    if (overwriteExistingSnapshot)
                    {
                        _fileService.WriteAllText(callerName, callerFilePath, json);
                    }
                    else
                    {
                        var filePath = _fileService.BuildFilePath(callerName, callerFilePath);
                        throw new SnapshotException(exception,
                            $"For this test to pass, the json in '{filePath}' needs to be updated.\n" +
                            "\n" +
                            $"'{filePath}' can be updated in two ways: \n" +
                            "    By running \'.Snapshot(T input, true)\', where true indicates that the existing snapshot should be overwritten.\n" +
                            $"    By manually editing '{filePath}'.\n");
                    }
                }
            }
            else
            {
                _fileService.WriteAllText(callerName, callerFilePath, json);
                // TODO: Alert test runner with a warning that a new snapshot json file has been created
            }
        }
    }
}