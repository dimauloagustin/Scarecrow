using Scarecrow.Core.Pipe;
using System.ComponentModel.DataAnnotations;
using Validator;
using Validator.interfaces;

namespace Scarecrow.Responses {
    public class ProfileResponse {
        [Required]
        public string Name { get; set; }
        [Required]
        public Rules[] Rules { get; set; }
        [Required]
        public Repository[] Repositories { get; set; }

        public ProfileResponse(Profile profile) {
            Name = profile.Name;
            Rules = profile.Rules.Select(r => new Rules(r)).ToArray();
            Repositories = profile.Repositories.Select(r => new Repository(r)).ToArray();
        }
    }

    public class Rules {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }

        public Rules(IValidation rule) {
            Type = rule.Type;
            Name = rule.Name;
        }
    }

    public class Repository {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }

        public Repository(RepositoryData rule) {
            Name = rule.Name;
            Url = rule.Url;
        }
    }
}
