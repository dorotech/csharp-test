namespace DoroTech.BookStore.Application.RequestHandlers.QueryHandlers;

public abstract class BaseQueryHandler<TRequest, TResponse> : BaseRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected BaseQueryHandler() : base()
    {
    }
}

