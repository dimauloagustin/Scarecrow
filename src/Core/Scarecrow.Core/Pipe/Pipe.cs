using GitClient.Interfaces;

namespace Scarecrow.Core.Pipe {
    public class Pipe {

        private readonly IGitClient _gitClient;
        private readonly Profile[] _profiles;

        public Pipe(IGitClient gitClient, Profile[] profiles) {
            _gitClient = gitClient;
            _profiles = profiles;
        }
    }
}