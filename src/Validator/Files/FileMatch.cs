using System.IO.Abstractions;
using Validator.interfaces;

namespace Validator.Files {
    public class FileMatch : ValidationBase, IValidation {

        readonly IFileSystem _fs;
        readonly string _expected;

        public FileMatch(string name, IFileSystem fs, string path, string expected) : base(name, ValidationTypes.FileMatch, path) {
            _fs = fs;
            _expected = expected;
        }

        public override async Task<RuleValidationResult> Execute(RepositoryData repo) {
            if (!_fs.File.Exists(repo.Path + _path)) return CreateResult("File not found in: " + repo.Path + _path);

            var file = await _fs.File.ReadAllTextAsync(repo.Path + _path);

            if (file != _expected) return CreateResult("File content didn't match");

            return CreateResult();
        }
    }
}
