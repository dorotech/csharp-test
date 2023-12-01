using FluentValidation;

namespace Application.Features.Publishers.Commands.Delete;

public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
{
    public DeletePublisherCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(default(Guid));
    }
}