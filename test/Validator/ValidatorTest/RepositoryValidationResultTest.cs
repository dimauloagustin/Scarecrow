using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Validator;
using Validator.Files;
using Xunit;

namespace ValidatorTest {
    public class RepositoryValidationResultTest {
        [Fact]
        public void Should_return_true_if_all_are_ok() {
            //Arrange
            var mockResults = new List<RuleValidationResult>();
            mockResults.Add(new RuleValidationResult("test", "test"));
            mockResults.Add(new RuleValidationResult("test", "test"));
            mockResults.Add(new RuleValidationResult("test", "test"));
            var uut = new RepositoryValidationResult(mockResults);

            //Act
            var res = uut.IsOk();

            //Assert
            Assert.True(res);
        }

        [Fact]
        public void Should_return_true_if_at_least_one_is_false() {
            //Arrange
            var mockResults = new List<RuleValidationResult>();
            mockResults.Add(new RuleValidationResult("test", "test"));
            mockResults.Add(new RuleValidationResult("test", "test", new List<string>()));
            mockResults.Add(new RuleValidationResult("test", "test"));
            var uut = new RepositoryValidationResult(mockResults);

            //Act
            var res = uut.IsOk();

            //Assert
            Assert.False(res);
        }
    }
}