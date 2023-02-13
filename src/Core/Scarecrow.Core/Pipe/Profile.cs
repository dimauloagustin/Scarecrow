using GitClient.Interfaces;
using Microsoft.DotNet.PlatformAbstractions;
using Validator;
using Validator.interfaces;

namespace Scarecrow.Core.Pipe {
    public class Profile {
        public string Name { get; private init; }
        private readonly IValidation[] _rules;
        private readonly RepositoryData[] _repositories;
        private readonly IGitClient _gitClient;

        public Profile(string name, IValidation[] rules, string[] repositories, IGitClient gitClient, string organization) {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            Name = name;
            _rules = rules;
            //TODO - other repos
            //TODO - change base path
            _repositories = repositories.Select(r => new RepositoryData(
                Path.Combine(ApplicationEnvironment.ApplicationBasePath, organization, r),
                "https://bitbucket.org/" + organization + "/" + r,
                r)).ToArray();
            _gitClient = gitClient;
        }

        public async Task<RuleValidationResult[]> ValidateRepo(string repoName) {
            var repo = _repositories.FirstOrDefault(r => r.Name == repoName);
            if (repo == null) throw new ArgumentException("repo: " + repo + " not found in profile");

            _gitClient.Clone(repo.Url, repo.Path);

            RuleValidationResult[] res = new RuleValidationResult[_rules.Length];
            for (int i = 0; i < _rules.Length; i++) {
                res[i] = await _rules[i].Execute(repo);
            }
            return res;
        }
    }
}