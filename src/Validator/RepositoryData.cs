using Validator.interfaces;

namespace Validator {
    public class RepositoryData {
        public string Path { get; init; }
        public string Url { get; init; }
        public string Name { get; init; }
        public RepositoryValidationResult? ValidationResult { get; private set; }
        public RepositoryValidationProgressStatus RepositoryValidationProgressStatus { get; private set; } = RepositoryValidationProgressStatus.NEVER_DONE;
        public DateTime? LastValidation { get; private set; } = null;

        public RepositoryData(string path, string url, string name) {
            Path = path;
            Url = url;
            Name = name;
        }

        public async Task ExecuteValidations(IValidation[] rules) {
            RepositoryValidationProgressStatus = RepositoryValidationProgressStatus.IN_PROGRESS;

            RuleValidationResult[] res = new RuleValidationResult[rules.Length];
            for (int i = 0; i < rules.Length; i++) {
                res[i] = await rules[i].Execute(this);
            }

            ValidationResult = new RepositoryValidationResult(res.ToList());

            LastValidation = DateTime.Now;
            RepositoryValidationProgressStatus = RepositoryValidationProgressStatus.FINISHED;
        }
    }
}
