using GitClient.Factory;
using Microsoft.DotNet.PlatformAbstractions;
using Moq;
using Scarecrow.Core.Pipe.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Validator.interfaces;
using Xunit;

namespace Scarecrow.Core.Test {
    public class PipeFactoryTest {
        [Fact]
        public void Should_return_pipe() {
            //Arrange
            var ruleMock = new Mock<IValidation>();
            ruleMock.SetupGet(r => r.Name).Returns("test");
            var rulesMapperMock = new Mock<IRulesMapper>();
            rulesMapperMock.Setup(s => s.Map("test", "test rule", It.Is<Dictionary<string, string>>(v => v.GetValueOrDefault("p2") == "[ \"v2\", \"v3\" ]")))
                .Returns(ruleMock.Object);
            var filePath = GetTestDataFolder("Pipe\\Factories\\testPipe.json");
            var json = File.ReadAllText(filePath);
            var uut = new PipeFactory(rulesMapperMock.Object, new Mock<GitClientFactory>(new MockFileSystem()).Object);

            //Act
            var res = uut.CreateFromJson(json);

            //Assert
            Assert.NotNull(res);
            rulesMapperMock.Verify(m => m.Map("test", "test rule", It.Is<Dictionary<string, string>>(v => v.GetValueOrDefault("p2") == "[ \"v2\", \"v3\" ]")));
        }

        [Fact]
        public void Should_fail_if_profile_name_not_found() {
            //Arrange
            var rulesMapperMock = new Mock<IRulesMapper>();
            var filePath = GetTestDataFolder("Pipe\\Factories\\testPipeNoProfileName.json");
            var json = File.ReadAllText(filePath);
            var uut = new PipeFactory(rulesMapperMock.Object, new Mock<GitClientFactory>(new MockFileSystem()).Object);

            //Act
            var res = () => uut.CreateFromJson(json);

            //Assert
            Assert.Throws<ArgumentNullException>(res);
        }

        public static string GetTestDataFolder(string testDataFolder) {
            string startupPath = ApplicationEnvironment.ApplicationBasePath;
            var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
            var pos = pathItems.Reverse().ToList().FindIndex(x => string.Equals("bin", x));
            string projectPath = string.Join(Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - pos - 1));
            return Path.Combine(projectPath, testDataFolder);
        }
    }
}