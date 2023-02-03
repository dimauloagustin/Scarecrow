namespace Validator {
    public class RuleValidationResult {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool IsOk { get; private set; }

        public RuleValidationResult(string name, string type, bool isOk) {
            Name = name;
            Type = type;
            IsOk = isOk;
        }
    }
}
