using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace Snapshot
{
    // TODO: Work out how to make this static and testable.
    public class SnapshotAssert
    {
        private readonly IFileService _fileService;

        public SnapshotAssert(IFileService fileService)
        {
            _fileService = fileService;
        }

        // TODO: Callername will only work for when directly called by the test method,
        // need to look at how to get name of the method in the stack trace which has the [Fact] attribute.
        // TODO: Work out how to get access to ITestHelperOutput and show output for test runner.
        // TODO: Work out how to access caller name and caller file path using reflection.
        public void Snapshot<T>(
            T input,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            var json = JsonConvert.SerializeObject(input);

            if (_fileService.Exists(callerName, callerFilePath))
            {
                var snapshotJson = _fileService.ReadAllText(callerName, callerFilePath);

                if (snapshotJson.Equals(json, StringComparison.Ordinal))
                {
                    Assert.True(true);
                }
                else
                {
                    // TODO: Warn user to copy test output into file if correct
                    // TODO: Find a better way of prompting the user to update the snapshot
                    Assert.True(false);
                }
            }
            else
            {
                _fileService.WriteAllText(callerName, callerFilePath, json);
                // TODO: Alert test runner with a warning
            }
        }
    }
}