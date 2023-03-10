using GitClient.Interfaces;
using Microsoft.DotNet.PlatformAbstractions;
using Validator;
using Validator.interfaces;

namespace Scarecrow.Core.Pipe {
    public class Profile {
        public string Name { get; private init; }
        public IValidation[] Rules { get; private init; }
        public RepositoryData[] Repositories { get; private init; }
        private readonly IGitClient _gitClient;

        public Profile(string name, IValidation[] rules, string[] repositories, IGitClient gitClient, string organization) {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            if (rules.GroupBy(x => x.Name).Any(g => g.Count() > 1)) throw new ArgumentException("Duplicated rule names are not allowed in profile " + name);

            Name = name;
            Rules = rules;
            //TODO - other repos
            //TODO - change base path
            Repositories = repositories.Select(r => new RepositoryData(
                Path.Combine(ApplicationEnvironment.ApplicationBasePath, organization, r),
                "https://bitbucket.org/" + organization + "/" + r,
                r)).ToArray();
            _gitClient = gitClient;
        }

        public async Task<RepositoryData> ValidateRepo(string repoName) {
            var repo = Repositories.FirstOrDefault(r => r.Name == repoName);
            if (repo == null) throw new ArgumentException("repo: " + repo + " not found in profile");
            _gitClient.Clone(repo.Url, repo.Path);
            await repo.ExecuteValidations(Rules);

            return repo;
        }
    }
}