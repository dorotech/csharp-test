using FluentValidation.Results;

namespace DoroTechChallenge.Services.Responses;

public class InsertOrUpdateResponse<T> where T : class
{
    public ValidationResult Result { get; }
    public T Entity { get; }
    public List<string> Errors { get; set; }

    private InsertOrUpdateResponse(ValidationResult result, T entity)
    {
        Result = result;
        Entity = entity;
    }

    public InsertOrUpdateResponse(List<string> errors)
    {
        Errors = errors;
    }

    public static InsertOrUpdateResponse<T> Valid(ValidationResult result, T entity) =>
        result.IsValid
            ? new InsertOrUpdateResponse<T>(result, entity)
            : throw new ArgumentException("Validation result should be valid!");

    public static InsertOrUpdateResponse<T> Invalid(ValidationResult result) =>
        result.IsValid
            ? throw new ArgumentException("Validation result should be invalid!")
            : new InsertOrUpdateResponse<T>(result, null);

    public static InsertOrUpdateResponse<T> Invalid(List<string> errors) =>
        errors.Count != 0
            ? new InsertOrUpdateResponse<T>(errors)
            : throw new ArgumentException($"{nameof(errors)} não deveria estar vazio");
}
