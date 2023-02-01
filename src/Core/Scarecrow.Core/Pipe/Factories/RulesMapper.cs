using System.IO.Abstractions;
using Validator.Files;
using Validator.interfaces;

namespace Scarecrow.Core.Pipe.Factories {
    public class RulesMapper {
        private readonly Dictionary<string, Func<Dictionary<string, string>, IValidation>> _mapper;

        public RulesMapper(IFileSystem fs, string basePath) {
            _mapper = new Dictionary<string, Func<Dictionary<string, string>, IValidation>> { //TODO - Improve code
                { "FileMatch", (p) => new FileMatch(fs, basePath, GetRequiredParam(p,"path","FileMatch"), GetRequiredParam(p,"expected","FileMatch")) },
                { "FileExistance", (p) => new FileExistance(fs, basePath, GetRequiredParam(p,"path","FileExistance")) }
            };
        }

        public IValidation Map(string type, Dictionary<string, string> parameter) {
            //TODO - validate type exists
            //TODO - validate params
            return _mapper[type](parameter);
        }

        private static string GetRequiredParam(Dictionary<string, string> parameter, string param, string rule) {
            //TODO - Validate params
            if (!parameter.ContainsKey(param)) throw new ArgumentNullException(param + " is required in rule " + rule);
            return parameter[param];
        }
    }
}