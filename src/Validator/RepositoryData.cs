namespace Validator {
    public class RepositoryData {
        public string Path { get; init; }
        public string Url { get; init; }
        public string Name { get; init; }
        public RepositoryValidationResult? ValidationResult { get; private set; }
        public DateTime? LastValidation { get; private set; }

        public RepositoryData(string path, string url, string name) {
            Path = path;
            Url = url;
            Name = name;
        }

        public void SetValidationResult(RepositoryValidationResult validationResult) {
            ValidationResult = validationResult;
            LastValidation = DateTime.Now;
        }
    }
}
