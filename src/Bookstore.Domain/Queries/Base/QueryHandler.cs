namespace Bookstore.Domain.Queries.Base;

public abstract class QueryHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : IRequest<TResult>
{
    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await HandleCommand(request, cancellationToken);
    }

    protected abstract Task<TResult> HandleCommand(TCommand request, CancellationToken cancellationToken);
}