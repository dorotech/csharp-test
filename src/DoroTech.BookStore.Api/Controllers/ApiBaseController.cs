using DoroTech.BookStore.Application.Common.Interfaces.Services;
using DoroTech.BookStore.Application.Exceptions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using ILogger = Serilog.ILogger;

namespace DoroTech.BookStore.Api.Controllers;

[ApiController]
public class ApiBaseController : ControllerBase
{
    protected ISender Mediator { get; }
    protected IMapper Mapper { get; }
    private readonly ILogger _logger;
    private readonly INotificationService _notificationService;

    public ApiBaseController(ISender mediator, ILogger logger, IMapper mapper, INotificationService notificationService)
    {
        Mediator = mediator;
        Mapper = mapper;
        _logger = logger;
        _notificationService = notificationService;
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
        ActionResult actionResult = error switch
        {
            BookStoreException e => StatusCode(e.StatusCode, Mapper.Map<BookStoreException, ProblemDetails>(e)),
            _ => _notificationService.HasErrors
                     ? HandleNotificationErrors(_notificationService.ProblemDetails)
                     : StatusCode(StatusCodes.Status500InternalServerError, GenerateDefaultError())
        };

        _logger
            .Error("RequestName: {EventName} | Info: {@Info} | Date: {TimeStamp} | Exception: {Exception}", nameof(HandleError), request, DateTimeOffset.UtcNow, error);

        return actionResult;
    }

    private ObjectResult HandleNotificationErrors(ProblemDetails problemDetails)
    {
        _logger.Information(nameof(HandleNotificationErrors), problemDetails);
        return StatusCode(StatusCodes.Status400BadRequest, problemDetails);
    }

    private ProblemDetails GenerateDefaultError()
       => new()
       {
           Type = "Internal Error",
           Title = "UNEXPECTED_ERROR",
           Status = StatusCodes.Status500InternalServerError,
       };
}
