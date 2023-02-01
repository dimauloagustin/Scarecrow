namespace Scarecrow.Core.Pipe.PipeConfigs {
    public class PipeConfig {
        public string RepoType { get; set; } = "";
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public string Organization { get; set; } = "";
        public ProfileConfig[] Profiles { get; set; } = Array.Empty<ProfileConfig>();
    }
}
