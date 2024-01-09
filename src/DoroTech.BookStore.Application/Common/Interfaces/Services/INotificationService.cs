namespace DoroTech.BookStore.Application.Common.Interfaces.Services;

public interface INotificationService
{
    bool HasErrors { get; }
    ProblemDetails ProblemDetails { get; }

    void AddErrors(ValidationResult validationResult);
}
