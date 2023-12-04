using FluentValidation;

namespace Bookstore.Domain.Dtos.v1.Request.Authentication;

public class GetUserTokenDtoValidator: AbstractValidator<GetUserTokenDto>
{
    public GetUserTokenDtoValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}