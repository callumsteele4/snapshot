using Newtonsoft.Json;
using Xunit.Sdk;

namespace Snapshot
{
    // Possibility: https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/
    public class Assert : Xunit.Assert
    {
        // TODO: Move filepath knowledge out of Snapshot and into FileService
        public static void Snapshot<T>(
            T input,
            bool overwriteExistingSnapshot = false)
        {
            var (testName, testFilePath) = FileService.GetTestMethodNameAndFilePath();
            var json = JsonConvert.SerializeObject(input);

            if (FileService.Exists(testName, testFilePath))
            {
                var snapshotJson = FileService.ReadAllText(testName, testFilePath);

                try
                {
                    Equal(snapshotJson, json);
                }
                catch (EqualException exception)
                {
                    if (overwriteExistingSnapshot)
                    {
                        FileService.WriteAllText(testName, testFilePath, json);
                    }
                    else
                    {
                        var filePath = FileService.BuildFilePath(testName, testFilePath);
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
                var filePath = FileService.BuildFilePath(testName, testFilePath);
                FileService.WriteAllText(testName, testFilePath, json);
                throw new SnapshotException(
                    $"A new json file has been written for this snapshot in '{filePath}'.\n" +
                    $"\n" +
                    $"The test has failed so that you are alerted to this fact, and will pass if run again.");
            }
        }
    }
}