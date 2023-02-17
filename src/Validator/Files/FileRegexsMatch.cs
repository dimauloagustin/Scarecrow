using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Validator.interfaces;

namespace Validator.Files {
    public class FileRegexsMatch : ValidationBase, IValidation {

        readonly IFileSystem _fs;
        readonly List<Regex> _regexs;

        public FileRegexsMatch(string name, IFileSystem fs, string path, List<string> regexs) : base(name, ValidationTypes.FileRegexsMatch, path) {
            _fs = fs;
            _regexs = regexs.Select(r => new Regex(r)).ToList();
        }

        public override async Task<RuleValidationResult> Execute(RepositoryData repo) {
            if (!_fs.File.Exists(repo.Path + _path)) return CreateResult("File not found in: " + repo.Path + _path);

            var lines = await _fs.File.ReadAllLinesAsync(repo.Path + _path);

            int regexIndex = 0;

            foreach (var line in lines) {
                if (regexIndex == _regexs.Count) return CreateResult();
                if (_regexs[regexIndex].IsMatch(line)) regexIndex++;
            }
            if (regexIndex == _regexs.Count) return CreateResult();

            return CreateResult("Regex did't match: " + _regexs[regexIndex].ToString());
        }
    }
}
