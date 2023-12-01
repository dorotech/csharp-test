using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Authors.Commands.Delete;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Authors.Handlers;

[Trait(nameof(DeleteAuthorCommand), "Handler")]
public class DeleteAuthorCommandHandlerTests : TestBase
{
    private readonly DeleteAuthorCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Author> _authorRepository;

    public DeleteAuthorCommandHandlerTests()
    {
        _handler = new DeleteAuthorCommandHandler(_unitOfWork);
        _authorRepository = A.Fake<IRepository<Domain.Entities.Author>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Author>()).Returns(_authorRepository);
    }

    [Fact]
    public async Task DeleteAuthorCommandHandler_AuthorNotRegistered_ReturnErrorAsync()
    {
        var command = new DeleteAuthorCommand{Id = Guid.NewGuid()};
        
        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Author));
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [Fact]
    public async Task DeleteAuthorCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new DeleteAuthorCommand{Id = Guid.NewGuid()};
        Domain.Entities.Author author = new Domain.Entities.Author(_faker.Person.FullName, default);;

        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(author);
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }
}