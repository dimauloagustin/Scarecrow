using Validator.interfaces;

namespace Scarecrow.Core.Pipe.Factories {
    public interface IRulesMapper {
        IValidation Map(string type, string name, Dictionary<string, string> parameters);
    }
}
