using MediatR;

namespace DoroTech.BookStore.Contracts.Requests.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
