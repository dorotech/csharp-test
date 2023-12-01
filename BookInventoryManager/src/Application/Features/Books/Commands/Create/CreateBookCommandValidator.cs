using FluentValidation;

namespace Application.Features.Books.Commands.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Title).NotEmpty();
        RuleFor(command => command.Edition).NotEmpty();
        RuleFor(command => command.Language).NotEmpty();
        RuleFor(command => command.PublicationDate).NotEqual(default(DateTime));
        RuleFor(command => command.AuthorId).NotEqual(default(Guid));
        RuleFor(command => command.PublisherId).NotEqual(default(Guid));
        RuleFor(command => command.CategoryId).NotEqual(default(Guid));
        RuleFor(command => command.CurrentInventory).GreaterThan(0);
        RuleFor(command => command.SalePrice).GreaterThan(0);
        RuleFor(command => command.Isbn).GreaterThan(0);

        When(command => command.PurchasePrice != null, () => { RuleFor(command => command.PurchasePrice).GreaterThan(0); });
        When(command => command.Height != null, () => { RuleFor(command => command.Height).GreaterThan(0); });
        When(command => command.Weight != null, () => { RuleFor(command => command.Weight).GreaterThan(0); });
        When(command => command.Length != null, () => { RuleFor(command => command.Length).GreaterThan(0); });
        When(command => command.Width != null, () => { RuleFor(command => command.Width).GreaterThan(0); });
    }
}