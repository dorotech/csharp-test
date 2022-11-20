using DoroTechChallenge.Models;
using FluentValidation;

namespace DoroTechChallenge.Services.Validators;

public class PublishingCompanyValidator : AbstractValidator<PublishingCompany>
{
    public PublishingCompanyValidator() 
    {
        RuleFor(pc => pc)
            .NotEmpty().WithMessage("The entity cannot be empty!")
            .NotNull().WithMessage("The entity cannot be empty!");
        RuleFor(pc => pc.CompanyName)
            .NotEmpty().WithMessage("Company name must be filled!")
            .NotNull().WithMessage("Company name must be filled!");
    }
}
