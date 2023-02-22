using GitClient.Clients.BitBucket;
using GitClient.Interfaces;
using System.IO.Abstractions;

namespace GitClient.Factory {
    public class GitClientFactory {
        public enum RepositoryTypes {
            BIT_BUCKET = 0
        }

        private Dictionary<RepositoryTypes, Func<string, string, IGitClient>> _gitClientBuilder;
        public GitClientFactory(IFileSystem fs) {
            _gitClientBuilder = new Dictionary<RepositoryTypes, Func<string, string, IGitClient>>() {
                {RepositoryTypes.BIT_BUCKET,(string user, string pass)=> new BitBucketClientAdapter(user,pass,fs)}
            };
        }

        public virtual IGitClient CreateClient(RepositoryTypes type, string user, string pass) {
            return _gitClientBuilder[type](user, pass);
        }
    }
}
