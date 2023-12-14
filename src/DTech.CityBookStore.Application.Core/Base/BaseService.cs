using DTech.CityBookStore.Application.Core.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace DTech.CityBookStore.Application.Core.Base;

public abstract class BaseService
{
    protected readonly INotifier _notifier;

    public BaseService(INotifier notifier)
    {
        _notifier = notifier;
    }

    public void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    public void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }

    public bool Validate<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE>
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }

    public bool IsValid() => !_notifier.HasNotification();

    public List<string> GetErrosMessages() => _notifier.GetNotifications().Select(n => n.Message).ToList();
}
