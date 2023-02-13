namespace Scarecrow.Core.Pipe {
    public class Pipe {

        public Profile[] Profiles { get; init; }

        public Pipe(Profile[] profiles) {
            Profiles = profiles;
        }
    }
}