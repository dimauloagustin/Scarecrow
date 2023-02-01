using Scarecrow.Core.Pipe.Factories;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace Scarecrow.Core.Test {
    public class RulesMapperTest {
        [Fact]
        public void Should_return_validation() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new RulesMapper(mockFs, "/test");

            //Act
            var res = uut.Map("FileExistance", new Dictionary<string, string>() { { "path", "test" } });

            //Assert
            Assert.NotNull(res);
        }

        [Fact]
        public void Should_fail_if_required_param_not_found() {
            //Arrange
            var mockFs = new MockFileSystem();
            var mockInputFile = new MockFileData("test1\ntest2\ntest3");
            mockFs.AddFile("/test/test.txt", mockInputFile);
            var uut = new RulesMapper(mockFs, "/test");

            //Act
            var res = () => uut.Map("FileMatch", new Dictionary<string, string>());

            //Assert
            Assert.Throws<ArgumentNullException>(res);
        }
    }
}