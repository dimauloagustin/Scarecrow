using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Validator.Files;
using Xunit;

namespace ValidatorTest.Files {
    public class FileRegexsMatchTest {
        [Fact]
        public async void Should_return_true_if_file_match_all_regexs_in_all_lines() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var regexs = new List<string>() { "test[1-9]", "test[1-9]", "test[1-9]" };
            var uut = new FileRegexsMatch("test", mockFs, "/test.txt", regexs);

            //Act
            var res = await uut.Execute(RepositoryDataMockHelper.GetMock("/test"));

            //Assert
            Assert.True(res.IsOk);
        }

        [Fact]
        public async void Should_return_true_if_file_match_all_regexs() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var regexs = new List<string>() { "test[1-9]", "test[1-9]" };
            var uut = new FileRegexsMatch("test", mockFs, "/test.txt", regexs);

            //Act
            var res = await uut.Execute(RepositoryDataMockHelper.GetMock("/test"));

            //Assert
            Assert.True(res.IsOk);
        }

        [Fact]
        public async void Should_return_false_if_file_not_match_all_regexs() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var regexs = new List<string>() { "test3", "test2" };
            var uut = new FileRegexsMatch("test", mockFs, "/test.txt", regexs);

            //Act
            var res = await uut.Execute(RepositoryDataMockHelper.GetMock("/test"));

            //Assert
            if (res.Errors == null) throw new NullReferenceException();
            Assert.False(res.IsOk);
            Assert.True(res.Errors.Count == 1);
            Assert.Equal("Regex did't match: test2", res.Errors.First());
        }

        [Fact]
        public async void Should_return_false_if_file_does_not_exists() {
            //Arrange
            var mockFs = new MockFileSystem();
            var regexs = new List<string>() { "test3", "test2" };
            var uut = new FileRegexsMatch("test", mockFs, "/test.txt", regexs);

            //Act
            var res = await uut.Execute(RepositoryDataMockHelper.GetMock("/test"));

            //Assert
            if (res.Errors == null) throw new NullReferenceException();
            Assert.False(res.IsOk);
            Assert.True(res.Errors.Count == 1);
            Assert.Equal("File not found in: /test/test.txt", res.Errors.First());
        }
    }
}