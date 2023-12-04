namespace Bookstore.Domain.Queries.Base;

public record Query<TResponse> : IRequest<TResponse>;