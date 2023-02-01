using GitClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Scarecrow.Responses;

namespace Scarecrow.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class RepositoryController : ControllerBase {
        private readonly IGitClient _gitClient;

        public RepositoryController(IGitClient gitClient) {
            _gitClient = gitClient;
        }

        [HttpGet]
        public async Task<List<RepositoryResponse>> Get() {
            return (await _gitClient.GetRepositories("pickit_it")).Select(r => new RepositoryResponse(r)).ToList();
        }
    }
}