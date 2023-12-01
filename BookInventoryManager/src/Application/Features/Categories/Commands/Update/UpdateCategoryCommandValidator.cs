using FluentValidation;

namespace Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
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