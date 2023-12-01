using Application.Features.Stock.Commands.Create;
using Domain.Enums;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Tests.Features.Stocks.Validators;

[Trait(nameof(CreateStockMovementCommand), "Validator")]
public class CreateStockMovementCommandTests
{
    private readonly IValidator<CreateStockMovementCommand> _validator = new CreateStockMovementCommandValidator();

    [Theory]
    [InlineData(0)]
    [InlineData(-547)]
    [InlineData(-1)]
    public void CreateStockMovementCommandValidator_InvalidQuantity_ReturnErrorAsync(int quantity)
    {
        var result = _validator.TestValidate(new CreateStockMovementCommand
        {
            Quantity = quantity
        });
        result.ShouldHaveValidationErrorFor(command => command.Quantity);
    }

    [Fact]
    public void CreateStockMovementCommandValidator_InvalidBookId_ReturnErrorAsync()
    {
        var result = _validator.TestValidate(new CreateStockMovementCommand
        {
            BookId = default(Guid)
        });
        result.ShouldHaveValidationErrorFor(command => command.Quantity);
    }

    [Fact]
    public void CreateStockMovementCommandValidator_IsValid_ReturnSuccessAsync()
    {
        var result = _validator.TestValidate(new CreateStockMovementCommand
        {
            BookId = Guid.NewGuid(),
            Quantity = 10,
            Description = "teste",
            Type = EMovementType.Incoming
        });

        result.ShouldNotHaveValidationErrorFor(command => command);
    }
}