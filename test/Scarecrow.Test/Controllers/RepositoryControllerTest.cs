using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Scarecrow.Test {
    public class RepositoryControllerTest
    : IClassFixture<WebApplicationFactory<Program>> {
        private readonly WebApplicationFactory<Program> _factory;

        public RepositoryControllerTest(WebApplicationFactory<Program> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task Should_return_repositories() {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Repository");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }
    }
}