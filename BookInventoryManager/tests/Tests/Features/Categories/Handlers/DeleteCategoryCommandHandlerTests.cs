using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Categories.Commands.Delete;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Categories.Handlers;

[Trait(nameof(DeleteCategoryCommand), "Handler")]
public class DeleteCategoryCommandHandlerTests : TestBase
{
    private readonly DeleteCategoryCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;

    public DeleteCategoryCommandHandlerTests()
    {
        _handler = new DeleteCategoryCommandHandler(_unitOfWork);
        _categoryRepository = A.Fake<IRepository<Domain.Entities.Category>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Category>()).Returns(_categoryRepository);
    }

    [Fact]
    public async Task DeleteCategoryCommand_CategoryNotRegistered_ReturnErrorAsync()
    {
        var command = new DeleteCategoryCommand{Id = Guid.NewGuid()};
        
        A.CallTo(_categoryRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Category));
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [Fact]
    public async Task DeleteCategoryCommand_IsValid_ReturnSuccessAsync()
    {
        var command = new DeleteCategoryCommand{Id = Guid.NewGuid()};
        Domain.Entities.Category category = new Domain.Entities.Category(_faker.Person.FullName, default);;

        A.CallTo(_categoryRepository.GetFirstOrDefaultAsyncFunc()).Returns(category);
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }
}