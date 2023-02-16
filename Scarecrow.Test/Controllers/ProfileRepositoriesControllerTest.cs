using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Scarecrow.Test {
    public class ProfileRepositoriesControllerTest
    : IClassFixture<WebApplicationFactory<Program>> {
        private readonly WebApplicationFactory<Program> _factory;

        public ProfileRepositoriesControllerTest(WebApplicationFactory<Program> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task Should_return_repositories() {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Profiles/Sample/Repositories");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }
    }
}