using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Snapshot.Tests
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public class AssertTests
    {
        private const string Callername = "callerName";
        private const string CallerFilePath = "callerPath";
        private const string FilePath = "/directory/file/path.json";
        private const string ExistingDifferentSnapshotJson = "{\"String\":\"Random String\"}";

        [Fact]
        public void Given_snapshot_file_does_not_exist_then_creates_new_snapshot_file_with_snapshot_json()
        {
        }

        [Fact]
        public void Given_snapshot_file_exists_and_json_matches_then_does_not_throw_snapshot_exception()
        {
        }

        [Fact]
        public void Given_snapshot_file_exists_and_json_does_not_match_then_throws_snapshot_exception()
        {
        }

        [Fact]
        public void
            Given_snapshot_file_exists_and_json_does_not_match_and_overwrite_toggled_on_then_overwrites_snapshot_file_with_snapshot_json()
        {
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class TestClass
        {
            public string String { get; set; } = "String";
        }
    }
}