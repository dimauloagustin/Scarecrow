using Moq;
using Validator;

namespace ValidatorTest.Files {
    internal class RepositoryDataMockHelper {
        internal static RepositoryData GetMock(string path) {
            var repoMock = new RepositoryData(path, "", "");
            return repoMock;
        }
    }
}
