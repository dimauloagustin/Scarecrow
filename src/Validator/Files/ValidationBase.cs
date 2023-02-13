using Validator.interfaces;

namespace Validator.Files {
    public abstract class ValidationBase : IValidation {
        public static string Type => nameof(ValidationBase);
        public string Name { get; init; }

        protected readonly string _path;

        public ValidationBase(string name, string path) {
            Name = name;
            _path = path;
        }

        public abstract Task<RuleValidationResult> Execute(RepositoryData repo);

        protected RuleValidationResult CreateResult(List<string>? errors = null) {
            return new RuleValidationResult(Name, Type, errors);
        }

        protected RuleValidationResult CreateResult(string error) {
            return new RuleValidationResult(Name, Type, new List<string>() { error });
        }
    }
}
