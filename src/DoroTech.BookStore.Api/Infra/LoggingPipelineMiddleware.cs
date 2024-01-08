namespace DoroTech.BookStore.Api.Infra;

public class LoggingPipelineMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LoggingPipelineMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext).ConfigureAwait(false);
            _logger.Information("EventType =>  {status} Event => {@object} => Timestamp:{timeStamp}", "Request", new { Request = httpContext.Request.Path }, DateTimeOffset.UtcNow);
        }
        catch (Exception exception)
        {
            var isAuthenticated = httpContext.Request.Headers.TryGetValue("Authorization", out var token);
            if (isAuthenticated)
                _logger.Error("EventType => {status} Event => {@requestName} => Timestamp: {timeStamp} => Exception: {@exception}", "Request", new { UserToken = token, Request = httpContext.Request.Path }, DateTimeOffset.UtcNow, exception);
            else
                _logger.Error("EventType => {status} Event => {@requestName} => Timestamp: {timeStamp} => Exception: {@exception}", "Request", new { Request = httpContext.Request.Path, Body = httpContext.Request.Body }, DateTimeOffset.UtcNow, exception);
            throw;
        }
    }
}
