namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public abstract class BaseCommandHandler<TRequest, TResponse> : BaseRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected BaseCommandHandler()
    {
    }
}

