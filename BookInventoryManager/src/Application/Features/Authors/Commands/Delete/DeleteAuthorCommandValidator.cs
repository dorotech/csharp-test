using FluentValidation;

namespace Application.Features.Authors.Commands.Delete;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(default(Guid));
    }
}