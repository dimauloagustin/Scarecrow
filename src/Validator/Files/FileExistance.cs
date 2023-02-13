using System.IO.Abstractions;
using Validator.interfaces;

namespace Validator.Files {
    public class FileExistance : ValidationBase, IValidation {
        public static new string Type => nameof(FileExistance);

        private readonly IFileSystem _fs;

        public FileExistance(string name, IFileSystem fs, string path) : base(name, path) {
            _fs = fs;
        }

        public override Task<RuleValidationResult> Execute(RepositoryData repo) {

            bool res = _fs.File.Exists(repo.Path + _path);

            if (!res) return Task.FromResult(CreateResult("File not found in: " + repo.Path + _path));

            return Task.FromResult(CreateResult());
        }
    }
}
