using GitClient.Interfaces;
using LibGit2Sharp;
using SharpBucket.V2;
using System.IO.Abstractions;

namespace GitClient.Clients.BitBucket {
    public class BitBucketClientAdapter : IGitClient {
        private readonly string _user;
        private readonly string _password;
        private readonly IFileSystem _fs;
        public BitBucketClientAdapter(string user, string password, IFileSystem fs) {
            _user = user;
            _password = password;
            _fs = fs;
        }

        public Task<List<Models.Repository>> GetRepositories(string organization) {
            var sharpBucket = new SharpBucketV2();
            sharpBucket.BasicAuthentication(_user, _password);
            var repos = sharpBucket.WorkspacesEndPoint().WorkspaceResource(organization).RepositoriesResource.EnumerateRepositories();
            return Task.FromResult(repos.Select(r => new Models.Repository(r.name, r.project.name)).ToList());
        }

        public void Clone(string repoUrl, string path) {
            if (_fs.Directory.Exists(path)) {
                Recurse(new DirectoryInfo(path));
                _fs.Directory.Delete(path, true);
            }

            var co = new CloneOptions {
                CredentialsProvider = (_url, _usr, _cred) => new UsernamePasswordCredentials { Username = _user, Password = _password }
            };
            Repository.Clone(repoUrl, path, co);
        }

        private void Recurse(DirectoryInfo directory) {
            foreach (FileInfo fi in directory.GetFiles()) {
                fi.IsReadOnly = false; // or true
            }

            foreach (DirectoryInfo subdir in directory.GetDirectories()) {
                Recurse(subdir);
            }
        }
    }
}
