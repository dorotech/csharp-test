using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Authors.Commands.Create;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Authors.Handlers;

[Trait(nameof(CreateAuthorCommand), "Handler")]
public class CreateAuthorCommandHandlerTests : TestBase
{
    private readonly CreateAuthorCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Author> _authorRepository;

    public CreateAuthorCommandHandlerTests()
    {
        _handler = new CreateAuthorCommandHandler(_mapper, _unitOfWork);
        _authorRepository = A.Fake<IRepository<Domain.Entities.Author>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Author>()).Returns(_authorRepository);
    }

    [Fact]
    public async Task CreateAuthorCommandHandler_AlreadyRegistered_ReturnErrorAsync()
    {
        var command = new CreateAuthorCommand(_faker.Person.FullName, default);
        Domain.Entities.Author author = new Domain.Entities.Author(command.Name, default);

        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(author);
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }
    
    [Fact]
    public async Task CreateAuthorCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new CreateAuthorCommand(_faker.Person.FullName, default);
        Domain.Entities.Author author = null;

        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Author));
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
    }
}