using System.Net;
using Application.Common.Interfaces.UnitOfWork;
using Application.Features.Publishers.Commands.Update;
using FakeItEasy;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Publishers.Handlers;

[Trait(nameof(UpdatePublisherCommand), "Handler")]
public class UpdatePublisherCommandHandlerTests : TestBase
{
    private readonly UpdatePublisherCommandHandler _handler;
    private readonly IRepository<Domain.Entities.Publisher> _publisherRepository;

    public UpdatePublisherCommandHandlerTests()
    {
        _handler = new UpdatePublisherCommandHandler(_mapper, _unitOfWork);
        _publisherRepository = A.Fake<IRepository<Domain.Entities.Publisher>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Publisher>()).Returns(_publisherRepository);
    }

    [Fact]
    public async Task UpdatePublisherCommandHandler_PublisherNotRegistered_ReturnErrorAsync()
    {
        var command = new UpdatePublisherCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdatePublisherData
            {
                Name = _faker.Person.FullName
            }
        };
        
        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(default(Domain.Entities.Publisher));
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [Fact]
    public async Task UpdatePublisherCommandHandler_ConflitName_ReturnErrorAsync()
    {
        var command = new UpdatePublisherCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdatePublisherData()
            {
                Name = _faker.Person.FullName
            }
        };
        
        Domain.Entities.Publisher publisher = new Domain.Entities.Publisher(_faker.Person.FullName);

        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).Returns(publisher);
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.Conflict, result.StatusCode);
    }
    
    [Fact]
    public async Task UpdatePublisherCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new UpdatePublisherCommand
        {
            Id = Guid.NewGuid(),
            Data = new UpdatePublisherData
            {
                Name = _faker.Person.FullName,
            }
        };

        Domain.Entities.Publisher publisherNotExists = null;
        Domain.Entities.Publisher publisher = new Domain.Entities.Publisher(_faker.Person.FullName);;

        A.CallTo(_publisherRepository.GetFirstOrDefaultAsyncFunc()).ReturnsNextFromSequence(publisher, publisherNotExists);
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
    }
}