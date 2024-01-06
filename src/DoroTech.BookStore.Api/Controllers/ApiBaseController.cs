using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using ILogger = Serilog.ILogger;

namespace DoroTech.BookStore.Api.Controllers;

[ApiController]
public class ApiBaseController : ControllerBase
{
    protected ISender Mediator { get; }
    private readonly ILogger _logger;

    public ApiBaseController(ISender mediator, ILogger logger)
    {
        Mediator = mediator;
        _logger = logger;
    }

    protected async Task<IActionResult> SendRequest<T>(IRequest<Result<T>> request, int statusCode = StatusCodes.Status200OK)
    {
        return await Mediator.Send(request).ConfigureAwait(false) switch
        {
            (true, var result, _) => StatusCode(statusCode, result),
            var (_, _, error) => HandleError(request, error)
        };
    }

    protected async Task<IActionResult> SendRequest(IRequest<Result> request, int statusCode = StatusCodes.Status200OK)
    {
        return await Mediator.Send(request).ConfigureAwait(false) switch
        {
            (true, _) => StatusCode(statusCode),
            var (_, error) => HandleError(request, error)
        };
    }

    protected ActionResult HandleError(object? request, Exception? error)
    {
        var actionResult = error switch
        {
            _ => StatusCode(500)
        };

        _logger
            .Error("RequestName: {EventName} | Info: {@Info} | Date: {TimeStamp} | Exception: {Exception}", nameof(HandleError), request, DateTimeOffset.UtcNow, error);

        return actionResult;
    }
}
