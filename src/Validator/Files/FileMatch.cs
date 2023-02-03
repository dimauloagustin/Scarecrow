using System.IO.Abstractions;
using Validator.interfaces;

namespace Validator.Files {
    public class FileMatch : IValidation {
        public static string Type => nameof(FileMatch);

        public string Name { get; init; }
        readonly IFileSystem _fs;
        readonly string _path;
        readonly string _expected;

        public FileMatch(string name, IFileSystem fs, string path, string expected) {
            Name = name;
            _fs = fs;
            _path = path;
            _expected = expected;
        }

        public async Task<bool> Execute(RepositoryData repo) {
            if (!_fs.File.Exists(repo.Path + _path)) return false;

            var file = await _fs.File.ReadAllTextAsync(repo.Path + _path);
            return file == _expected;
        }
    }
}
