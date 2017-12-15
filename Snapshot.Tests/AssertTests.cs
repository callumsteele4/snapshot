using System.Diagnostics.CodeAnalysis;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Xunit.Sdk;

namespace Snapshot.Tests
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public class AssertTests
    {
        private const string Callername = "callerName";
        private const string CallerFilePath = "callerPath";
        private readonly SnapshotAssert _sut;
        private readonly Mock<IFileService> _mockFileService;

        public AssertTests()
        {
            _mockFileService = new Mock<IFileService>();
            _sut = new SnapshotAssert(_mockFileService.Object);
        }

        [Fact]
        public void Given_snapshot_file_does_not_exist_then_creates_new_snapshot_file_with_snapshot_json()
        {
            var snapshotee = new TestClass();

            _mockFileService
                .Setup(x => x.Exists(Callername, CallerFilePath))
                .Returns(false);

            _sut.Snapshot(snapshotee, Callername, CallerFilePath);

            _mockFileService
                .Verify(x => x.WriteAllText(
                        Callername,
                        CallerFilePath,
                        JsonConvert.SerializeObject(snapshotee)),
                    Times.Once);
        }

        [Fact]
        public void Given_snapshot_file_exists_and_json_matches_then_succeed()
        {
            var snapshotee = new TestClass();
            var snapshoteeJson = JsonConvert.SerializeObject(snapshotee);

            _mockFileService
                .Setup(x => x.Exists(Callername, CallerFilePath))
                .Returns(true);
            _mockFileService
                .Setup(x => x.ReadAllText(Callername, CallerFilePath))
                .Returns(snapshoteeJson);
            
            _sut.Snapshot(snapshotee, Callername, CallerFilePath);
        }

        [Fact]
        public void Given_snapshot_file_exists_and_json_does_not_match_then_fail()
        {
            var snapshotee = new TestClass();

            _mockFileService
                .Setup(x => x.Exists(Callername, CallerFilePath))
                .Returns(true);
            _mockFileService
                .Setup(x => x.ReadAllText(Callername, CallerFilePath))
                .Returns("random json");

            Assert.Throws<TrueException>(() => _sut.Snapshot(snapshotee, Callername, CallerFilePath));
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class TestClass
        {
            public string String { get; set; } = "String";
        }
    }
}