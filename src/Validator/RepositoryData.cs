namespace Validator {
    public class RepositoryData {
        public string Path { get; private set; }
        public string Url { get; private set; }
        public string Name { get; private set; }


        public RepositoryData(string path, string url, string name) {
            Path = path;
            Url = url;
            Name = name;
        }
    }
}
