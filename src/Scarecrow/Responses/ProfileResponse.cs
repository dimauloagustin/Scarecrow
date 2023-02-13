using Scarecrow.Core.Pipe;
using Validator;
using Validator.interfaces;

namespace Scarecrow.Responses {
    public class ProfileResponse {
        public string Name { get; set; }
        public Rules[] Rules { get; set; }
        public Repository[] Repositories { get; set; }

        public ProfileResponse(Profile profile) {
            Name = profile.Name;
            Rules = profile.Rules.Select(r => new Rules(r)).ToArray();
            Repositories = profile.Repositories.Select(r => new Repository(r)).ToArray();
        }
    }

    public class Rules {
        public string Type { get; set; }
        public string Name { get; set; }

        public Rules(IValidation rule) {
            Type = rule.Type;
            Name = rule.Name;
        }
    }

    public class Repository {
        public string Name { get; set; }
        public string Url { get; set; }

        public Repository(RepositoryData rule) {
            Name = rule.Name;
            Url = rule.Url;
        }
    }
}
