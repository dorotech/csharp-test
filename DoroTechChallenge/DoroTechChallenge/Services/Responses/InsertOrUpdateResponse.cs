using FluentValidation.Results;

namespace DoroTechChallenge.Services.Responses;

public class InsertOrUpdateResponse<T> where T : class
{
    public ValidationResult Result { get; }
    public bool Succeeded => Result.IsValid;
    public IReadOnlyCollection<string> Errors { get; }
    public T Entity { get; }

    private InsertOrUpdateResponse(ValidationResult result, T entity)
    {
        Result = result;
        Errors = result.Errors
            .Select(x => x.ErrorMessage)
            .ToList()
            .AsReadOnly();
        Entity = entity;
    }

    public static InsertOrUpdateResponse<T> Valid(ValidationResult result, T entity) =>
        result.IsValid
            ? new InsertOrUpdateResponse<T>(result, entity)
            : throw new ArgumentException("Validation result should be valid!");

    public static InsertOrUpdateResponse<T> Invalid(ValidationResult result) =>
        result.IsValid
            ? throw new ArgumentException("Validation result should be invalid!")
            : new InsertOrUpdateResponse<T>(result, null);
}
