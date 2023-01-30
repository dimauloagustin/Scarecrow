using System.IO.Abstractions.TestingHelpers;
using Validator.Files;
using Xunit;

namespace ValidatorTest {
    public class FileMatchTest {
        [Fact]
        public async void Should_return_true_if_file_match() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var pattern ="test1\ntest2\ntest3";
            var uut = new FileMatch(mockFs, "/test", "/test.txt", pattern);

            //Act
            var res = await uut.Execute();

            //Assert
            Assert.True(res);
        }

        [Fact]
        public async void Should_return_false_if_file_does_not_match() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var pattern = "test1\ntest2\ntest4";
            var uut = new FileMatch(mockFs, "/test", "/test.txt", pattern);

            //Act
            var res = await uut.Execute();

            //Assert
            Assert.False(res);
        }

        [Fact]
        public async void Should_return_false_if_file_does_not_exists() {
            //Arrange
            var mockFs = new MockFileSystem();
            var pattern = "test1\ntest2\ntest4";
            var uut = new FileMatch(mockFs, "/test", "/test.txt", pattern);

            //Act
            var res = await uut.Execute();

            //Assert
            Assert.False(res);
        }
    }
}