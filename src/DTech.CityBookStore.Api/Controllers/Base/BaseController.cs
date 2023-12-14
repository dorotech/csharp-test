using DTech.CityBookStore.Application.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace DTech.CityBookStore.Api.Controllers.Base;

[Produces("application/json")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly INotifier _notifier;

    public BaseController(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected ActionResult CustomResponse(object? result = null, bool isNotFound = false)
    {
        if (IsValid())
        {
            return Ok(result);
        }

        var erros = _notifier.GetNotifications().Select(s => s.Message).ToArray();

        var resultObject = new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            {
                "Messages",
                erros
            }
        });

        return isNotFound ? NotFound(resultObject) : BadRequest(resultObject);        
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErrorInvalidModel(modelState);
        return CustomResponse();
    }

    protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(errorMsg);
        }
    }

    protected void NotifyError(string mensagem) => _notifier.Handle(new Notification(mensagem));

    protected bool IsValid() => !_notifier.HasNotification();
}
