using System.IO.Abstractions;
using Validator.interfaces;

namespace Validator.Files {
    public class FileMatch : IValidation {
        readonly IFileSystem _fs;
        readonly string _basePath;
        readonly string _path;
        readonly string _expected;

        public FileMatch(IFileSystem fs, string basePath, string path, string expected) {
            _fs = fs;
            _basePath = basePath;
            _path = path;
            _expected = expected;
        }

        public async Task<bool> Execute() {
            if (!_fs.File.Exists(_basePath + _path)) return false;

            var file = await _fs.File.ReadAllTextAsync(_basePath + _path);
            return file == _expected;
        }
    }
}
