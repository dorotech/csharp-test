using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Categories.Commands.Create;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Categories.Handlers;

[Trait(nameof(CreateCategoryCommand), "Handler")]
public class CreateCategoryCommandHandlerTests : TestBase
{
    private readonly CreateCategoryCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Category> _categoryRespository;

    public CreateCategoryCommandHandlerTests()
    {
        _handler = new CreateCategoryCommandHandler(_mapper, _unitOfWork);
        _categoryRespository = A.Fake<IRepository<Domain.Entities.Category>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Category>()).Returns(_categoryRespository);
    }

    [Fact]
    public async Task CreateCategoryCommandHandler_AlreadyRegistered_ReturnErrorAsync()
    {
        var command = new CreateCategoryCommand(_faker.Commerce.Categories(1)[0], default);
        Domain.Entities.Category category = new Domain.Entities.Category(_faker.Commerce.Categories(1)[0], _faker.Lorem.Sentence());

        A.CallTo(_categoryRespository.GetFirstOrDefaultAsyncFunc()).Returns(category);

        var result = await _handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }

    [Fact]
    public async Task CreateCategoryCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new CreateCategoryCommand(_faker.Commerce.Categories(1)[0], default);

        A.CallTo(_categoryRespository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Category));

        var result = await _handler.Handle(command, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
    }
}