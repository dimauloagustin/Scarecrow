namespace Scarecrow.Core.Pipe.PipeConfigs {
    public class ProfileConfig {
        public string Name { get; set; } = "";
        public RuleConfig[] Rules { get; set; } = Array.Empty<RuleConfig>();
        public string[] Repositories { get; set; } = Array.Empty<string>();
    }
}
