namespace Bookstore.Domain.Commands.Base;

public record Command<TResponse> : IRequest<TResponse>;