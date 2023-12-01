using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Categories.Commands.Update;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Categories.Handlers;

[Trait(nameof(UpdateCategoryCommand), "Handler")]
public class UpdateCategoryCommandHandlerTests : TestBase
{
    private readonly UpdateCategoryCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;

    public UpdateCategoryCommandHandlerTests()
    {
        _handler = new UpdateCategoryCommandHandler(_mapper, _unitOfWork);
        _categoryRepository = A.Fake<IRepository<Domain.Entities.Category>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Category>()).Returns(_categoryRepository);
    }

    [Fact]
    public async Task UpdateCategoryCommandHandler_CategoryNotRegistered_ReturnErrorAsync()
    {
        var command = new UpdateCategoryCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateCategoryData()
            {
                Name = _faker.Commerce.Categories(1)[0]
            }
        };
        
        A.CallTo(_categoryRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Category));
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [Fact]
    public async Task UpdateCategoryCommandHandler_ConflitName_ReturnErrorAsync()
    {
        var command = new UpdateCategoryCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateCategoryData()
            {
                Name = _faker.Commerce.Categories(1)[0]
            }
        };
        
        Domain.Entities.Category category = new Domain.Entities.Category(_faker.Person.FullName, default);;

        A.CallTo(_categoryRepository.GetFirstOrDefaultAsyncFunc()).Returns(category);
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }
    
    [Fact]
    public async Task UpdateCategoryCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new UpdateCategoryCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateCategoryData()
            {
                Name = _faker.Commerce.Categories(1)[0],
                Description = _faker.Lorem.Sentence()
            }
        };

        Domain.Entities.Category categoryNotExists = null;
        Domain.Entities.Category category = new Domain.Entities.Category(_faker.Person.FullName, default);;

        A.CallTo(_categoryRepository.GetFirstOrDefaultAsyncFunc()).ReturnsNextFromSequence(category, categoryNotExists);
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
    }
}