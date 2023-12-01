using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Publishers.Commands.Delete;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Publishers.Handlers;

[Trait(nameof(DeletePublisherCommand), "Handler")]
public class DeletePublisherCommandHandlerTests : TestBase
{
    private readonly DeletePublisherCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Publisher> _publisherRepository;

    public DeletePublisherCommandHandlerTests()
    {
        _handler = new DeletePublisherCommandHandler(_unitOfWork);
        _publisherRepository = A.Fake<IRepository<Domain.Entities.Publisher>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Publisher>()).Returns(_publisherRepository);
    }

    [Fact]
    public async Task DeletePublisherCommandHandler_PublisherNotRegistered_ReturnErrorAsync()
    {
        var command = new DeletePublisherCommand{Id = Guid.NewGuid()};

        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Publisher));
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [Fact]
    public async Task DeletePublisherCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new DeletePublisherCommand{Id = Guid.NewGuid()};
        Domain.Entities.Publisher publisher = new Domain.Entities.Publisher(_faker.Person.FullName);;

        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(publisher);
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }
}