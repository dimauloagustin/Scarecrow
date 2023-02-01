using GitClient.Interfaces;
using LibGit2Sharp;
using SharpBucket.V2;

namespace GitClient.Clients.GitHub {
    public class BitBucketClientAdapter : IGitClient {
        private readonly string _user;
        private readonly string _password;
        public BitBucketClientAdapter(string user, string password) {
            _user = user;
            _password = password;
        }

        public Task<List<Models.Repository>> GetRepositories(string organization) {
            var sharpBucket = new SharpBucketV2();
            sharpBucket.BasicAuthentication(_user, _password);
            var repos = sharpBucket.WorkspacesEndPoint().WorkspaceResource(organization).RepositoriesResource.EnumerateRepositories();
            return Task.FromResult(repos.Select(r => new Models.Repository(r.name, r.project.name)).ToList());
        }

        public void Clone(string repoUrl, string path) {
            var co = new CloneOptions {
                CredentialsProvider = (_url, _usr, _cred) => new UsernamePasswordCredentials { Username = _user, Password = _password }
            };
            Repository.Clone(repoUrl, path, co);
        }
    }
}
