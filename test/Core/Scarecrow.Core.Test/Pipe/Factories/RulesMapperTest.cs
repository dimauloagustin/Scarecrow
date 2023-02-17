using Scarecrow.Core.Pipe.Factories;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text.Json;
using Validator.Files;
using Xunit;

namespace Scarecrow.Core.Test {
    public class RulesMapperTest {
        [Fact]
        public void Should_return_validation() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new RulesMapper(mockFs);

            //Act
            var res = uut.Map("FileExistance", "test", new Dictionary<string, string>() { { "path", "test" } });

            //Assert
            Assert.NotNull(res);
        }

        [Fact]
        public void Should_return_validation_FileRegexsMatch() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new RulesMapper(mockFs);

            //Act
            var res = uut.Map("FileRegexsMatch", "test", new Dictionary<string, string>() { { "path", "test" }, { "regexs", "[\"test\",\"test\"]" } });

            //Assert
            Assert.IsType<FileRegexsMatch>(res);
        }

        [Fact]
        public void Should_fail_if_required_param_not_found() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new RulesMapper(mockFs);

            //Act
            var res = () => uut.Map("FileMatch", "test", new Dictionary<string, string>());

            //Assert
            Assert.Throws<ArgumentException>(res);
        }

        [Fact]
        public void Should_fail_if_required_param_is_null() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new RulesMapper(mockFs);

            //Act
            var res = () => uut.Map("FileRegexsMatch", "test", new Dictionary<string, string>() { { "path", "test" }, { "regexs", "null" } });

            //Assert
            Assert.Throws<ArgumentException>(res);
        }
    }
}