using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Scarecrow.Test {
    public class ProfileRepositoriesControllerTest
    : IClassFixture<TestWebApplicationFactory<Program>> {
        private readonly TestWebApplicationFactory<Program> _factory;

        public ProfileRepositoriesControllerTest(TestWebApplicationFactory<Program> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task Should_return_profile_repositories() {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Profiles/Sample/Repositories");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }

        [Fact]
        public async Task Should_return_Ok_and_execute_validations() {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/Profiles/Sample/Repositories/ms-logisticoperator-rest/Validation", null);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task Should_return_Ok_and_execute_validations_and_return_results_after_task_ends() {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/Profiles/Sample/Repositories/ms-logisticoperator-rest/Validation", null);
            response.EnsureSuccessStatusCode();
            System.Threading.Thread.Sleep(5000);
            response = await client.GetAsync("/Profiles/Sample/Repositories/ms-logisticoperator-rest");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Contains("\"isOk\":false", await response.Content.ReadAsStringAsync());
        }
    }
}