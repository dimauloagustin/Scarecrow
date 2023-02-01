using GitClient.Models;

namespace GitClient.Interfaces {
    public interface IGitClient {
        Task<List<Repository>> GetRepositories(string organization);
        void Clone(string repoUrl, string path);
    }
}
