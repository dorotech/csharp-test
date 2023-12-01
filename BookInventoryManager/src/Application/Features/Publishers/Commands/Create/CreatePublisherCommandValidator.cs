using FluentValidation;

namespace Application.Features.Publishers.Commands.Create;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty();
    }
}