using System.IO.Abstractions;
using Validator.interfaces;

namespace Validator.Files {
    public class FileExistance : IValidation {
        public static string Type => nameof(FileExistance);
        public string Name { get; init; }

        readonly IFileSystem _fs;
        readonly string _path;

        public FileExistance(string name, IFileSystem fs, string path) {
            Name = name;
            _fs = fs;
            _path = path;
        }

        public Task<bool> Execute(RepositoryData repo) {
            return Task.FromResult(_fs.File.Exists(repo.Path + _path));
        }
    }
}
