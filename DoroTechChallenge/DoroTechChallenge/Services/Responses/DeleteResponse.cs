namespace DoroTechChallenge.Services.Responses;

public class DeleteResponse<T>
{
    public bool Success { get; }
    public int ResourceId { get; }
    public IReadOnlyList<string> Errors { get; }

    private DeleteResponse(int resourceId, List<string> errors)
    {
        Success = errors.Count == 0;
        ResourceId = resourceId;
        Errors = errors.AsReadOnly();
    }

    public static DeleteResponse<T> Valid(int resourceId) =>
       new(resourceId, new List<string>());

    public static DeleteResponse<T> Invalid(List<string> errors) =>
        errors.Count != 0
            ? new DeleteResponse<T>(0, errors)
            : throw new ArgumentException($"{nameof(errors)} não deveria estar vazio");
}
