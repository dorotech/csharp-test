using System.Net;
using Application.Common.Constants;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            return (TResponse)Activator.CreateInstance(typeof(TResponse),
                CommonConstants.UnexpectedErrorMessage,
                HttpStatusCode.InternalServerError);
        }
    }
}
