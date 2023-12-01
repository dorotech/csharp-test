using Application.Features.Categories.Commands.Update;
using FluentValidation;

namespace Application.Features.Authors.Commands.Update;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
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