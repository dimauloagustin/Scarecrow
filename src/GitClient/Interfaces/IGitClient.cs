using GitClient.Models;

namespace GitClient.Interfaces {
    public interface IGitClient {
        Task<List<GitRepository>> GetRepositories(string organization);
        void Clone(string repoUrl, string path);
    }
}
