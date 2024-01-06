using MediatR;

namespace DoroTech.BookStore.Application.RequestHandlers;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected BaseRequestHandler()
    {
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
