using Validator.interfaces;

namespace Validator.Files {
    public class FileExistance : IValidation {
        string _basePath;
        string _path;

        public FileExistance(string basePath, string path) {
            _basePath = basePath;
            _path = path;
        }

        public bool Execute() {
            return File.Exists(_basePath + _path);
        }
    }
}
