using FluentValidation;

namespace Application.Features.Books.Commands.Update;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(command => command.Id).NotEqual(default(Guid));
        RuleFor(command => command.Data.PublicationDate).NotEqual(default(DateTime));
        RuleFor(command => command.Data.AuthorId).NotEqual(default(Guid));
        RuleFor(command => command.Data.PublisherId).NotEqual(default(Guid));
        RuleFor(command => command.Data.CategoryId).NotEqual(default(Guid));

        When(command => command.Data.Isbn != null, () => { RuleFor(command => command.Data.Isbn).GreaterThan(0); });
        When(command => command.Data.Edition != null, () => { RuleFor(command => command.Data.Edition).NotEmpty(); });
        When(command => command.Data.Language != null, () => { RuleFor(command => command.Data.Language).NotEmpty(); });
        When(command => command.Data.Title != null, () => { RuleFor(command => command.Data.Title).NotEmpty(); });
        When(command => command.Data.PurchasePrice != null, () => { RuleFor(command => command.Data.PurchasePrice).GreaterThan(0); });
        When(command => command.Data.SalePrice != null, () => { RuleFor(command => command.Data.SalePrice).GreaterThan(0); });
        When(command => command.Data.Height != null, () => { RuleFor(command => command.Data.Height).GreaterThan(0); });
        When(command => command.Data.Weight != null, () => { RuleFor(command => command.Data.Weight).GreaterThan(0); });
        When(command => command.Data.Length != null, () => { RuleFor(command => command.Data.Length).GreaterThan(0); });
        When(command => command.Data.Width != null, () => { RuleFor(command => command.Data.Width).GreaterThan(0); });
    }
}