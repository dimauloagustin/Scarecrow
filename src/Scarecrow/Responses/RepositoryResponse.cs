using GitClient.Models;

namespace Scarecrow.Responses {
    public class RepositoryResponse {
        public string Name { get; set; }
        public string? Project { get; set; }

        public RepositoryResponse(Repository repo) {
            Name = repo.Name;
            Project = repo.Project;
        }
    }
}
