using Validator.interfaces;

namespace Scarecrow.Core.Pipe {
    public class Profile {
        public string Name { get; private init; }
        private readonly IValidation[] _rules;
        private readonly string[] _repositories;

        public Profile(string name, IValidation[] rules, string[] repositories) {
            Name = name;
            _rules = rules;
            _repositories = repositories;
        }
    }
}