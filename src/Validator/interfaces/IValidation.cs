namespace Validator.interfaces {
    public interface IValidation {
        static string Type { get; } = "";
        public string Name { get; }
        Task<RuleValidationResult> Execute(RepositoryData repo);
    }
}
