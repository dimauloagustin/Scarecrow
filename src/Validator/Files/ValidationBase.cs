using Validator.interfaces;

namespace Validator.Files {
    public abstract class ValidationBase : IValidation {
        public string Type { get; init; }
        public string Name { get; init; }

        protected readonly string _path;

        protected ValidationBase(string name, string type, string path) {
            Name = name;
            Type = type;
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
