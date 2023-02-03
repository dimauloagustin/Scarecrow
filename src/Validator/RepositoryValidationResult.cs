namespace Validator {
    public class RepositoryValidationResult {

        private readonly List<RuleValidationResult> _ruleValidationResult = new List<RuleValidationResult>();

        public bool IsOk() {
            return !_ruleValidationResult.Any(v => v.IsOk);
        }
    }
}
