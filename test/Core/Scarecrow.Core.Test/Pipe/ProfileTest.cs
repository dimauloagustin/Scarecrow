using GitClient.Clients.BitBucket;
using Scarecrow.Core.Pipe;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Validator.Files;
using Validator.interfaces;
using Xunit;

namespace Scarecrow.Core.Test.Pipe {
    public class ProfileTest {

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
            Assert.True(res.Length == 1);
            Assert.True(res[0]);
        }
    }
}
