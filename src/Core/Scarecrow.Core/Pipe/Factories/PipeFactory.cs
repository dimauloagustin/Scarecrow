using GitClient.Clients.GitHub;
using Scarecrow.Core.Pipe.PipeConfigs;
using System.Text.Json;

namespace Scarecrow.Core.Pipe.Factories {
    public class PipeFactory {
        private readonly IRulesMapper _rulesMapper;

        public PipeFactory(IRulesMapper rulesMapper) {
            _rulesMapper = rulesMapper;
        }

        public Pipe CreateFromJson(string json) {
            var config = JsonSerializer.Deserialize<PipeConfig>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            if (config == null) throw new ArgumentException("invalid json");

            var profiles = config.Profiles.Select(p => new Profile(p.Name, p.Rules.Select(r => _rulesMapper.Map(r.Name, r.Params)).ToArray(), p.Repositories));

            //TODO - other types of clients
            return new Pipe(new BitBucketClientAdapter(config.User, config.Password), profiles.ToArray());
        }
    }
}