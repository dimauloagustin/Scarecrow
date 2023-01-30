using System.IO.Abstractions;
using Validator.interfaces;

namespace Validator.Files {
    public class FileExistance : IValidation {
        readonly IFileSystem _fs;
        readonly string _basePath;
        readonly string _path;

        public FileExistance(IFileSystem fs, string basePath, string path) {
            _fs = fs;
            _basePath = basePath;
            _path = path;
        }

        public Task<bool> Execute() {
            return Task.FromResult(_fs.File.Exists(_basePath + _path));
        }
    }
}
