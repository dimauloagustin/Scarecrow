using Microsoft.AspNetCore.Mvc;
using Scarecrow.Core.Pipe;
using Scarecrow.Responses;

namespace Scarecrow.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : ControllerBase {
        private readonly Pipe _pipe;

        public ProfilesController(Pipe pipe) {
            _pipe = pipe;
        }

        [HttpGet]
        public ActionResult<List<ProfileResponse>> Get() {
            return Ok(_pipe.Profiles.Select(r => new ProfileResponse(r)).ToList());
        }
    }
}