using System.ComponentModel.DataAnnotations;
using Validator;

namespace Scarecrow.Responses {
    public class ProfileRepositoryResponse {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Url { get; init; }
        [Required]
        public RepositoryValidationProgressStatus RepositoryValidationProgressStatus { get; init; }
        public DateTime? LastValidation { get; init; }
        public bool? IsOk { get; init; }

        public ProfileRepositoryResponse(RepositoryData repo) {
            Name = repo.Name;
            Url = repo.Url;
            RepositoryValidationProgressStatus = repo.RepositoryValidationProgressStatus;
            LastValidation = repo.LastValidation;
            IsOk = repo.ValidationResult?.IsOk();
        }
    }
}
