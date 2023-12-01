using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Publishers.Commands.Create;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Publishers.Handlers;

[Trait(nameof(CreatePublisherCommand), "Handler")]
public class CreatePublisherCommandHandlerTests : TestBase
{
    private readonly CreatePublisherCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Publisher> _publisherRepository;

    public CreatePublisherCommandHandlerTests()
    {
        _handler = new CreatePublisherCommandHandler(_mapper, _unitOfWork);
        _publisherRepository = A.Fake<IRepository<Domain.Entities.Publisher>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Publisher>()).Returns(_publisherRepository);
    }

    [Fact]
    public async Task CreatePublisherCommandHandler_AlreadyRegistered_ReturnErrorAsync()
    {
        var command = new CreatePublisherCommand(_faker.Person.FullName, default);
        Domain.Entities.Publisher publisher = new Domain.Entities.Publisher(command.Name);

        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(publisher);
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }
    
    [Fact]
    public async Task CreatePublisherCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new CreatePublisherCommand(_faker.Person.FullName, default);

        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Publisher));
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
    }
}