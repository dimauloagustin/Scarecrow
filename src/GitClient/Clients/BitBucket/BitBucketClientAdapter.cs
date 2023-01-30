using GitClient.Interfaces;
using LibGit2Sharp;

namespace GitClient.Clients.GitHub {
    public class BitBucketClientAdapter : IGitClient {
        private readonly string _user;
        private readonly string _password;
        public BitBucketClientAdapter(string user, string password) {
            _user = user;
            _password = password;
        }

        public void Clone(string repoUrl, string path) {
            var co = new CloneOptions {
                CredentialsProvider = (_url, _usr, _cred) => new UsernamePasswordCredentials { Username = _user, Password = _password }
            };
            Repository.Clone(repoUrl, path, co);
        }
    }
}
