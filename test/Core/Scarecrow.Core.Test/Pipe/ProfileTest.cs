using GitClient.Clients.BitBucket;
using Scarecrow.Core.Pipe;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Validator.Files;
using Validator.interfaces;
using Xunit;

namespace Scarecrow.Core.Test.Pipe {
    public class ProfileTest {

        [Fact]
        public async Task Should_throw_exception_on_duplicated_rule_name() {
            //Arrange
            var rules = new List<IValidation>() {
                { new FileExistance("test", new FileSystem(), "/Dockerfile") },
                { new FileExistance("test", new FileSystem(), "/Dockerfile") }
            }.ToArray();

            //Act
            var action = () => new Profile("test", rules, new List<string>() { "ms-logisticoperator-rest" }.ToArray(),
                new BitBucketClientAdapter("agustin_di_maulo", "ATBBuwUrNKbSgnZMmRWytxDZXfECA4454D44", new FileSystem()),
                "pickit_it");

            //Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public async Task Should_run_validations_on_repoAsync() {
            //Arrange
            var uut = new Profile(
                "test profile",
                new List<IValidation>() { { new FileExistance("test", new FileSystem(), "/Dockerfile") } }.ToArray(),
                new List<string>() { "ms-logisticoperator-rest" }.ToArray(),
                new BitBucketClientAdapter("agustin_di_maulo", "ATBBuwUrNKbSgnZMmRWytxDZXfECA4454D44", new FileSystem()),
                "pickit_it");

            //Act
            var res = await uut.ValidateRepo("ms-logisticoperator-rest");

            //Assert
            Assert.True(res.ValidationResult?.RuleValidationResults.Count == 1);
            Assert.True(res.ValidationResult?.RuleValidationResults[0].IsOk);
        }
    }
}
