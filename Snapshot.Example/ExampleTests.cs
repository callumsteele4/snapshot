using Xunit;
using Xunit.Sdk;

namespace Snapshot.Example
{
    public class ExampleTests
    {
        private readonly SnapshotAssert _snapshotAssert;

        public ExampleTests()
        {
            _snapshotAssert = new SnapshotAssert();
        }
        
        // This is an example test, the snapshot JSON can be seen in /__snapshots/ExampleTests/Snapshot_check_snapshot_matches.json
        // JSON is as seen below:
        // {"String":"String","Int":10}
        [Fact]
        public void Snapshot_check_snapshot_matches()
        {
            var testClass = new TestClass();
            
            _snapshotAssert.Snapshot(testClass);
        }
        
        // This is an example test, the snapshot JSON can be seen in /__snapshots/ExampleTests/Snapshot_throws_for_non_matching_snapshot.json
        // JSON is as seen below:
        // {"String":"String","Int":10}
        [Fact]
        public void Snapshot_throws_for_non_matching_snapshot()
        {
            var testClass = new TestClass {String = "New string"};
            Assert.Throws<TrueException>(() => _snapshotAssert.Snapshot(testClass));
        }

        private class TestClass
        {
            public string String { get; set; } = "String";
            public int Int { get; set; } = 10;
        }
    }
}