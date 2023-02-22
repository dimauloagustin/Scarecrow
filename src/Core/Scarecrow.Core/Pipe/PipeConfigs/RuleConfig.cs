using System.Text.Json;

namespace Scarecrow.Core.Pipe.PipeConfigs {
    public class RuleConfig {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public Dictionary<string, string> Params { get; set; } = new();
    }
}
