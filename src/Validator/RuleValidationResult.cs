namespace Validator {
    public class RuleValidationResult {
        public string Name { get; init; }
        public string Type { get; init; }
        public bool IsOk { get; init; }
        public List<string>? Errors { get; init; }

        public RuleValidationResult(string name, string type, List<string>? errors = null) {
            Name = name;
            Type = type;
            if (errors == null) {
                IsOk = true;
            } else {
                Errors = errors;
                IsOk = false;
            }
        }
    }
}
