using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Snapshot.Example
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class Example
    {
        public string Message { get; set; } = "Initial message.";

        public void Update()
        {
            Message = "Updated the message.";
        }
    }
    
    public class ExampleTests
    {
        private readonly Example _example;

        public ExampleTests()
        {
            _example = new Example();
        }
        
        // This is an example test, the snapshot JSON can be seen in /__snapshots/ExampleTests/Snapshot_check_snapshot_matches.json
        // JSON is as seen below:
        // {"Message":"Initial message."}
        [Fact]
        public void Check_initial_message()
        {
            Assert.Snapshot(_example);
        }
        
        // This is an example test, the snapshot JSON can be seen in /__snapshots/ExampleTests/Snapshot_throws_for_non_matching_snapshot.json
        // JSON is as seen below:
        // {"Message":"Updated the message."}
        [Fact]
        public void Check_message_after_update_called()
        {
            _example.Update();

            Assert.Snapshot(_example);
        }

        // This is an example test, the snapshot JSON can be seen in /__snapshots/ExampleTests/Check_message_in_another_method.json
        // The name of the method with the [Fact] attribute will be used for the snapshot JSON filename, rather than the calling method.
        // JSON is as seen below:
        // {"Message":"Initial message."}
        [Fact]
        public void Check_message_in_another_method()
        {
            CheckSnapshot(_example);
        }

        private void CheckSnapshot(Example example)
        {
            Assert.Snapshot(example);
        }
    }
}