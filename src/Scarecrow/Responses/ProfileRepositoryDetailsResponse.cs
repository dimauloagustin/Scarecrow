using System.ComponentModel.DataAnnotations;
using Validator;

namespace Scarecrow.Responses {
    public class ProfileRepositoryDetailsResponse {
        [Required]
        public List<RuleValidationResult> RuleResults { get; init; }

        public ProfileRepositoryDetailsResponse(RepositoryValidationResult result) {
            RuleResults = result.RuleValidationResults;
        }

        public class RuleValidationResultResponse {
            [Required]
            public string Name { get; init; }
            [Required]
            public string Type { get; init; }
            [Required]
            public bool IsOk { get; init; }
            [Required]
            public List<string>? Errors { get; init; }

            public RuleValidationResultResponse(RuleValidationResult result) {
                Name = result.Name;
                Type = result.Type;
                IsOk = result.IsOk;
                Errors = result.Errors;
            }
        }
    }
}
