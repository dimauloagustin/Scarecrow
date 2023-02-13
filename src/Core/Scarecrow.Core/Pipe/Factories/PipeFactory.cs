using GitClient.Clients.BitBucket;
using Scarecrow.Core.Pipe.PipeConfigs;
using System.IO.Abstractions;
using System.Text.Json;

namespace Scarecrow.Core.Pipe.Factories {
    public class PipeFactory {
        private readonly IRulesMapper _rulesMapper;
        private readonly IFileSystem _fs;

        public PipeFactory(IRulesMapper rulesMapper, IFileSystem fs) {
            _rulesMapper = rulesMapper;
            _fs = fs;
        }

        public Pipe CreateFromJson(string json) {
            var config = JsonSerializer.Deserialize<PipeConfig>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            if (config == null) throw new ArgumentException("invalid json");

            var profiles = config.Profiles.Select(p => new Profile(p.Name, p.Rules.Select(r => _rulesMapper.Map(r.Type, r.Name, r.Params)).ToArray(), p.Repositories, new BitBucketClientAdapter(config.User, config.Password, _fs), config.Organization));

            //TODO - other types of clients
            return new Pipe(profiles.ToArray());
        }
    }
}