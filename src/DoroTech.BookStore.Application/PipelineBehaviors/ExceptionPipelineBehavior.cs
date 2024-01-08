using System.Reflection;
using MediatR;
using OperationResult;

namespace DoroTech.BookStore.Application.PipelineBehaviors;
public sealed class ExceptionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest
{
    private readonly MethodInfo? _operationResultError;
    private readonly Type _type = typeof(TResponse);
    private readonly Type _typeOperationResult = typeof(Result);

    public ExceptionPipelineBehavior()
    {
        if (!_type.IsGenericType)
            return;

        _operationResultError = _typeOperationResult
            .GetMethods()
            .FirstOrDefault(m => m.Name == "Error" && m.IsGenericMethod)?
            .MakeGenericMethod(_type.GetGenericArguments().First());
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return _type == _typeOperationResult
                ? (TResponse)Convert.ChangeType(Result.Error(ex), _type)!
                : (TResponse)Convert.ChangeType(_operationResultError?.Invoke(null, new object[] { ex }), _type)!;
        }
    }
}
