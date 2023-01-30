namespace GitClient.Interfaces {
    public interface IGitClient {
        void Clone(string repoUrl, string path);
    }
}
