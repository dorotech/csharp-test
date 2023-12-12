using DTech.CityBookStore.Application.Core.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace DTech.CityBookStore.Application.Core.Base;

public abstract class BaseService
{
    private readonly INotifier _notifier;

    public BaseService(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }

    protected bool Validate<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE>
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }

    protected bool IsValid() => _notifier.HasNotification();
}
