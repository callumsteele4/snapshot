using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Sdk;

namespace Snapshot
{
    // TODO: Work out how to make this part of the Assert partial class and testable as such
    public class SnapshotAssert
    {
        private readonly IFileExistenceChecker _fileExistenceChecker;
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;

        public SnapshotAssert()
        {
            _fileExistenceChecker = new SnapshotFileExistenceChecker();
            _fileReader = new SnapshotFileReader();
            _fileWriter = new SnapshotFileWriter(new SnapshotDirectoryService());
        }

        internal SnapshotAssert(IFileExistenceChecker fileExistenceChecker, IFileReader fileReader, IFileWriter fileWriter)
        {
            _fileExistenceChecker = fileExistenceChecker;
            _fileReader = fileReader;
            _fileWriter = fileWriter;
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
            var callerMethodInfo = new CallerMethodInfo(callerName, callerFilePath);
            var json = JsonConvert.SerializeObject(input);

            if (_fileExistenceChecker.Exists(callerMethodInfo))
            {
                var snapshotJson = _fileReader.ReadAllText(callerMethodInfo);

                try
                {
                    Assert.Equal(snapshotJson, json);
                }
                catch (EqualException exception)
                {
                    if (overwriteExistingSnapshot)
                    {
                        _fileWriter.WriteAllText(callerMethodInfo, json);
                    }
                    else
                    {
                        throw new SnapshotException(exception,
                            $"For this test to pass, the json in '{callerMethodInfo.SnapshotFilePath}' needs to be updated.\n" +
                            "\n" +
                            $"'{callerMethodInfo.SnapshotFilePath}' can be updated in two ways: \n" +
                            "    By running \'.Snapshot(T input, true)\', where true indicates that the existing snapshot should be overwritten.\n" +
                            $"    By manually editing '{callerMethodInfo.SnapshotFilePath}'.\n");
                    }
                }
            }
            else
            {
                _fileWriter.WriteAllText(callerMethodInfo, json);
                // TODO: Alert test runner with a warning that a new snapshot json file has been created
            }
        }
    }
}