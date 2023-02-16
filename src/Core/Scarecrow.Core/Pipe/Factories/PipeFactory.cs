using GitClient.Factory;
using Scarecrow.Core.Pipe.PipeConfigs;
using System.Text.Json;

namespace Scarecrow.Core.Pipe.Factories {
    public class PipeFactory {
        private readonly IRulesMapper _rulesMapper;
        private readonly GitClientFactory _gitClientFactory;

        public PipeFactory(IRulesMapper rulesMapper, GitClientFactory gitClientFactory) {
            _rulesMapper = rulesMapper;
            _gitClientFactory = gitClientFactory;
        }

        public Pipe CreateFromJson(string json) {
            var config = JsonSerializer.Deserialize<PipeConfig>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            if (config == null) throw new ArgumentException("invalid json");

            var profiles = config.Profiles.Select(p => new Profile(p.Name, p.Rules.Select(r => _rulesMapper.Map(r.Type, r.Name, r.Params)).ToArray(), p.Repositories, _gitClientFactory.CreateClient(GitClientFactory.RepositoryTypes.BIT_BUCKET, config.User, config.Password), config.Organization));

            //TODO - other types of clients
            return new Pipe(profiles.ToArray());
        }
    }
}