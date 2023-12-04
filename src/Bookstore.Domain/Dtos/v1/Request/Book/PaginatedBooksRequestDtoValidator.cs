using FluentValidation;

namespace Bookstore.Domain.Dtos.v1.Request.Book;

public class PaginatedBooksRequestDtoValidator : AbstractValidator<PaginatedBooksRequestDto>
{
    public PaginatedBooksRequestDtoValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.Limit).GreaterThan(0);
    }
} 