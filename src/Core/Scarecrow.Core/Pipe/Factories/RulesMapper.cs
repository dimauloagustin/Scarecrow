using System.IO.Abstractions;
using System.Text.Json;
using Validator.Files;
using Validator.interfaces;

namespace Scarecrow.Core.Pipe.Factories {
    public class RulesMapper : IRulesMapper {
        private readonly Dictionary<string, Func<string, Dictionary<string, string>, IValidation>> _mapper;

        public RulesMapper(IFileSystem fs) {
            _mapper = new Dictionary<string, Func<string, Dictionary<string, string>, IValidation>> { //TODO - Improve code
                { "FileMatch", (n,p) => new FileMatch(n,fs, GetRequiredParam<string>(p,"path"), GetRequiredParam<string>(p,"expected")) },
                { "FileExistance", (n,p) => new FileExistance(n,fs, GetRequiredParam<string>(p,"path")) },
                { "FileRegexsMatch", (n,p) => new FileRegexsMatch(n,fs, GetRequiredParam<string>(p,"path"),GetRequiredParam<List<string>>(p,"path")) },
            };
        }

        public IValidation Map(string type, string name, Dictionary<string, string> parameters) {
            try {
                var map = _mapper.GetValueOrDefault(type);
                if (map == null) throw new InvalidOperationException("Rule type not found");
                return map(name, parameters);
            } catch (ArgumentException ex) {
                throw new ArgumentException(ex.Message + " - " + type + " - " + name, ex);
            }
        }

        private static T GetRequiredParam<T>(Dictionary<string, string> parameter, string paramName) {
            var param = parameter.GetValueOrDefault(paramName);
            if (param == null) throw new ArgumentException(paramName + " is required");

            if (typeof(T) == typeof(string)) return (T)(object)param;
            return JsonSerializer.Deserialize<T>(param);
        }
    }
}