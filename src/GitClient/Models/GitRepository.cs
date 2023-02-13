namespace GitClient.Models{
    public class GitRepository {
        public string Name { get; set; }
        public string? Project { get; set; }

        public GitRepository(string name, string? project) {
            Name = name;
            Project = project;
        }
    }
}
