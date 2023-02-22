namespace Validator.interfaces {
    public interface IValidation {
        string Type { get; }
        string Name { get; }
        Task<RuleValidationResult> Execute(RepositoryData repo);
    }
}
