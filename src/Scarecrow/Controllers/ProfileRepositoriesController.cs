using Microsoft.AspNetCore.Mvc;
using Scarecrow.Core.Pipe;
using Scarecrow.Responses;

namespace Scarecrow.Controllers {
    [ApiController]
    [Route("Profiles/{name}/Repositories")]
    public class ProfileRepositoriesController : ControllerBase {
        private readonly Pipe _pipe;

        public ProfileRepositoriesController(Pipe pipe) {
            _pipe = pipe;
        }

        [HttpGet]
        public ActionResult<List<ProfileRepositoryResponse>> GetAllRepositories(string name) {
            return Ok(_pipe.Profiles.Single(p => p.Name == name).Repositories.Select(r => new ProfileRepositoryResponse(r)).ToList());
        }

        [HttpGet("{repoName}")]
        public ActionResult<ProfileRepositoryDetailsResponse> GetRepository(string name, string repoName) {
            var res = _pipe.Profiles.Single(p => p.Name == name).Repositories.Single(r => r.Name == repoName).ValidationResult;
            if (res == null) return NotFound();

            return Ok(new ProfileRepositoryDetailsResponse(res));
        }

        [HttpPost("{repoName}/Validation")]
        public ActionResult ExecuteRepositoryValidation(string name, string repoName) {
            _ = Task.Run(() => _pipe.Profiles.Single(p => p.Name == name).ValidateRepo(repoName));
            return Ok();
        }
    }
}