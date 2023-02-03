using Moq;
using System.IO.Abstractions.TestingHelpers;
using Validator;
using Validator.Files;
using Xunit;

namespace ValidatorTest.Files {
    public class FileExistanceTest {
        [Fact]
        public async void Should_return_true_if_file_exists() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new FileExistance("test", mockFs, "/test.txt");

            //Act
            var res = await uut.Execute(RepositoryDataMockHelper.GetMock("/test"));

            //Assert
            Assert.True(res);
        }

        [Fact]
        public async void Should_return_false_if_file_does_not_exists() {
            //Arrange
            var mockFs = new MockFileSystem();
            var uut = new FileExistance("test", mockFs, "/test.txt");

            //Act
            var res = await uut.Execute(RepositoryDataMockHelper.GetMock("/test"));

            //Assert
            Assert.False(res);
        }
    }
}