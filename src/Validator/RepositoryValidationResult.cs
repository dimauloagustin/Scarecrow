namespace Validator {
    public class RepositoryValidationResult {

        public List<RuleValidationResult> RuleValidationResults { get; init; }

        public RepositoryValidationResult(List<RuleValidationResult> ruleValidationResult) {
            RuleValidationResults = ruleValidationResult;
        }

        public bool IsOk() {
            return !RuleValidationResults.Any(v => !v.IsOk);
        }
    }
}
