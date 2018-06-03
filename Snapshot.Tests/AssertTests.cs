using System.Diagnostics.CodeAnalysis;
using System.IO;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Snapshot.Tests
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public class AssertTests
    {
        private const string CallerName = "callerName";
        private const string CallerFilePath = "caller/file/path.cs";
        private const string SnapshotDirectoryPath = "caller/file/__snapshots__/path";
        private const string ExistingDifferentSnapshotJson = "{\"String\":\"Random String\"}";
        private readonly SnapshotAssert _sut;
        private readonly Mock<IFileExistenceChecker> _mockFileExistenceChecker;
        private readonly Mock<IFileReader> _mockFileReader;
        private readonly Mock<IFileWriter> _mockFileWriter;
        private readonly Mock<IDirectoryCreator> _mockDirectoryCreator;

        private readonly string _snapshotFilePath =
            SnapshotDirectoryPath + Path.DirectorySeparatorChar + CallerName + ".json";

        public AssertTests()
        {
            _mockFileExistenceChecker = new Mock<IFileExistenceChecker>();
            _mockFileReader = new Mock<IFileReader>();
            _mockFileWriter = new Mock<IFileWriter>();
            _mockDirectoryCreator = new Mock<IDirectoryCreator>();
            _sut = new SnapshotAssert(_mockFileExistenceChecker.Object, _mockFileReader.Object, _mockFileWriter.Object,
                _mockDirectoryCreator.Object);
        }

        [Fact]
        public void Given_snapshot_file_does_not_exist_then_creates_new_snapshot_file_with_snapshot_json()
        {
            var snapshotee = new TestClass();

            _mockFileExistenceChecker
                .Setup(x => x.Exists(It.Is<CallerMethodInfo>(callerMethodInfo =>
                    callerMethodInfo.SnapshotFilePath == _snapshotFilePath)))
                .Returns(false);

            _sut.Snapshot(snapshotee, false, CallerName, CallerFilePath);

            _mockDirectoryCreator
                .Verify(x => x.CreateDirectory(SnapshotDirectoryPath), Times.Once);
            _mockFileWriter.Verify(
                x => x.WriteAllText(
                    It.Is<CallerMethodInfo>(callerMethodInfo => callerMethodInfo.SnapshotFilePath == _snapshotFilePath),
                    JsonConvert.SerializeObject(snapshotee)),
                Times.Once);
        }

        [Fact]
        public void Given_snapshot_file_exists_and_json_matches_then_does_not_throw_snapshot_exception()
        {
            var snapshotee = new TestClass();
            var snapshoteeJson = JsonConvert.SerializeObject(snapshotee);

            _mockFileExistenceChecker
                .Setup(x => x.Exists(It.Is<CallerMethodInfo>(callerMethodInfo =>
                    callerMethodInfo.SnapshotFilePath == _snapshotFilePath)))
                .Returns(true);
            _mockFileReader
                .Setup(x => x.ReadAllText(It.Is<CallerMethodInfo>(callerMethodInfo =>
                    callerMethodInfo.SnapshotFilePath == _snapshotFilePath)))
                .Returns(snapshoteeJson);

            _sut.Snapshot(snapshotee, false, CallerName, CallerFilePath);
        }

        [Fact]
        public void Given_snapshot_file_exists_and_json_does_not_match_then_throws_snapshot_exception()
        {
            var snapshotee = new TestClass();

            _mockFileExistenceChecker
                .Setup(x => x.Exists(It.Is<CallerMethodInfo>(callerMethodInfo =>
                    callerMethodInfo.SnapshotFilePath == _snapshotFilePath)))
                .Returns(true);
            _mockFileReader
                .Setup(x => x.ReadAllText(It.Is<CallerMethodInfo>(callerMethodInfo =>
                    callerMethodInfo.SnapshotFilePath == _snapshotFilePath)))
                .Returns(ExistingDifferentSnapshotJson);

            Assert.Throws<SnapshotException>(() => _sut.Snapshot(snapshotee, false, CallerName, CallerFilePath));
        }

        [Fact]
        public void
            Given_snapshot_file_exists_and_json_does_not_match_and_overwrite_toggled_on_then_overwrites_snapshot_file_with_snapshot_json()
        {
            var snapshotee = new TestClass();

            _mockFileExistenceChecker
                .Setup(x => x.Exists(It.Is<CallerMethodInfo>(callerMethodInfo =>
                    callerMethodInfo.SnapshotFilePath == _snapshotFilePath)))
                .Returns(true);
            _mockFileReader
                .Setup(x => x.ReadAllText(It.Is<CallerMethodInfo>(callerMethodInfo =>
                    callerMethodInfo.SnapshotFilePath == _snapshotFilePath)))
                .Returns(ExistingDifferentSnapshotJson);

            _sut.Snapshot(snapshotee, true, CallerName, CallerFilePath);

            _mockFileWriter
                .Verify(x => x.WriteAllText(
                        It.Is<CallerMethodInfo>(callerMethodInfo =>
                            callerMethodInfo.SnapshotFilePath == _snapshotFilePath),
                        JsonConvert.SerializeObject(snapshotee)),
                    Times.Once);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class TestClass
        {
            public string String { get; set; } = "String";
        }
    }
}