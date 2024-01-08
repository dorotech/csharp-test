namespace DoroTech.BookStore.Application.PipelineBehaviors;

public sealed partial class FailFastRequestPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : struct
{
    private readonly IValidator<TRequest>? _validator;
    private readonly INotificationService _notificationService;

    public FailFastRequestPipelineBehavior(INotificationService notificationService, IValidator<TRequest>? validator = default)
        => (_validator, _notificationService) = (validator, notificationService);

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
            return next!.Invoke();

        var validationResult = _validator.Validate(request);
        if (validationResult.IsValid)
            return next!.Invoke();

        _notificationService.AddErrors(validationResult);

        return Task.FromResult((TResponse)default);
    }
}
