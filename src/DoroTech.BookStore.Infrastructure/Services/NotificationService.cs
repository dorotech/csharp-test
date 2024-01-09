using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace DoroTech.BookStore.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly List<ValidationFailure> _notifications;

    public NotificationService() => _notifications = [];

    public void AddErrors(ValidationResult validationResult)
        => _notifications.AddRange(validationResult.Errors);

    public bool HasErrors
        => _notifications.Any();

    public ProblemDetails ProblemDetails
    {
        get
        {
            var problemDetails = new ProblemDetails
            {
                Title = "INVALID_PARAMETERS",
                Type = "invalid_parameters",
                Status = StatusCodes.Status400BadRequest,
            };
            problemDetails.Extensions.Add("invalid-params", _notifications.Select(x => new ErrorDetailMessageResponse { Field = x.PropertyName, Message = x.ErrorMessage }));

            return problemDetails;
        }
    }
}
