using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Scarecrow.Test {
    public class ProfilesControllerTest
    : IClassFixture<WebApplicationFactory<Program>> {
        private readonly WebApplicationFactory<Program> _factory;

        public ProfilesControllerTest(WebApplicationFactory<Program> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task Should_return_profiles() {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Profiles");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }
    }
}