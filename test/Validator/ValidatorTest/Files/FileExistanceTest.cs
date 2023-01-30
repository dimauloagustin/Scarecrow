using System.IO.Abstractions.TestingHelpers;
using Validator.Files;
using Xunit;

namespace ValidatorTest {
    public class FileExistanceTest {
        [Fact]
        public void Should_return_true_if_file_exists() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new FileExistance(mockFs, "/test", "/test.txt");

            //Act
            var res = uut.Execute();

            //Assert
            Assert.True(res);
        }

        [Fact]
        public void Should_return_false_if_file_does_not_exists() {
            //Arrange
            var mockFs = new MockFileSystem();
            var uut = new FileExistance(mockFs, "/test", "/test.txt");

            //Act
            var res = uut.Execute();

            //Assert
            Assert.False(res);
        }
    }
}