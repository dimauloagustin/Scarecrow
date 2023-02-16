using GitClient.Factory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Scarecrow.Test.Infrastructure.GitClient;
using System;
using System.IO.Abstractions;
using System.Linq;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class {
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            var gitClientFactory = services.Single(
                d => d.ServiceType ==
                    typeof(GitClientFactory));

            services.Remove(gitClientFactory);

            services.AddSingleton<GitClientFactory>(container => {
                var factory = new Mock<GitClientFactory>(container.GetRequiredService<IFileSystem>());
                factory.Setup(f => f.CreateClient(It.IsAny<GitClientFactory.RepositoryTypes>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new TestClientAdapter(container.GetRequiredService<IFileSystem>()));

                return factory.Object;
            });
        });

        builder.UseEnvironment("Development");
    }
}