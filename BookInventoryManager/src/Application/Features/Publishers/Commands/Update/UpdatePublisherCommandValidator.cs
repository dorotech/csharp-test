using FluentValidation;

namespace Application.Features.Publishers.Commands.Update;

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(default(Guid));

        RuleFor(command => command.Data)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(commanda => commanda.Data.Name)
                    .NotEmpty();
            });
    }
}