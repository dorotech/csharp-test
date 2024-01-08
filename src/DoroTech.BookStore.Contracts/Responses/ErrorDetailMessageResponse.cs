namespace DoroTech.BookStore.Contracts.Responses;

public record struct ErrorDetailMessageResponse
{
    public string Field { get; init; }
    public string Message { get; init; }
}
