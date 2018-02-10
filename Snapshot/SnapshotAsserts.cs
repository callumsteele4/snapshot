using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Xunit.Sdk;

namespace Snapshot
{
    // Possibility: https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/
    public class Assert : Xunit.Assert
    {
        // TODO: Callername will only work for when directly called by the test method,
        // need to look at how to get name of the method in the stack trace which has the [Fact] attribute.
        // Work out how to access caller name and caller file path using reflection.
        public static void Snapshot<T>(
            T input,
            bool overwriteExistingSnapshot = false,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerFilePath = "")
        {
            var json = JsonConvert.SerializeObject(input);

            if (FileService.Exists(callerName, callerFilePath))
            {
                var snapshotJson = FileService.ReadAllText(callerName, callerFilePath);

                try
                {
                    Equal(snapshotJson, json);
                }
                catch (EqualException exception)
                {
                    if (overwriteExistingSnapshot)
                    {
                        FileService.WriteAllText(callerName, callerFilePath, json);
                    }
                    else
                    {
                        var filePath = FileService.BuildFilePath(callerName, callerFilePath);
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
                FileService.WriteAllText(callerName, callerFilePath, json);
                // TODO: Alert test runner with a warning that a new snapshot json file has been created
            }
        }
    }
}