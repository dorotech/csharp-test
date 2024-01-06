using MediatR;

namespace DoroTech.BookStore.Contracts.Requests.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
