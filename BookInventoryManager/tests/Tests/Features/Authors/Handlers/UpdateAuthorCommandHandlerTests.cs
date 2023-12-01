using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Authors.Commands.Update;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Authors.Handlers;

[Trait(nameof(UpdateAuthorCommand), "Handler")]
public class UpdateAuthorCommandHandlerTests : TestBase
{
    private readonly UpdateAuthorCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Author> _authorRepository;

    public UpdateAuthorCommandHandlerTests()
    {
        _handler = new UpdateAuthorCommandHandler(_mapper, _unitOfWork);
        _authorRepository = A.Fake<IRepository<Domain.Entities.Author>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Author>()).Returns(_authorRepository);
    }

    [Fact]
    public async Task UpdateAuthorCommandHandler_AuthorNotRegistered_ReturnErrorAsync()
    {
        var command = new UpdateAuthorCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateAuthorData
            {
                Name = _faker.Person.FullName
            }
        };
        
        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Author));
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [Fact]
    public async Task UpdateAuthorCommandHandler_ConflitName_ReturnErrorAsync()
    {
        var command = new UpdateAuthorCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateAuthorData
            {
                Name = _faker.Person.FullName
            }
        };
        
        Domain.Entities.Author author = new Domain.Entities.Author(_faker.Person.FullName, default);;

        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).Returns(author);
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }
    
    [Fact]
    public async Task UpdateAuthorCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new UpdateAuthorCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdateAuthorData
            {
                Name = _faker.Person.FullName,
                Biography = _faker.Lorem.Sentence()
            }
        };

        Domain.Entities.Author authorNotExists = null;
        Domain.Entities.Author author = new Domain.Entities.Author(_faker.Person.FullName, default);;

        A.CallTo(_authorRepository.GetFirstOrDefaultAsyncFunc()).ReturnsNextFromSequence(author, authorNotExists);
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
    }
}