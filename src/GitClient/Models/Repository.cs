namespace GitClient.Models{
    public class Repository {
        public string Name { get; set; }
        public string? Project { get; set; }

        public Repository(string name, string? project) {
            Name = name;
            Project = project;
        }
    }
}
