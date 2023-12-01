using Application.Common.Interfaces.UnitOfWork;
using AutoMapper;
using Bogus;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class TestBase
{
    private protected IMapper _mapper;
    private protected IUnitOfWork _unitOfWork;
    private protected Faker _faker;

    public TestBase()
    {
        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(Application.DependencyInjection).Assembly);
        var serviceProvider = services.BuildServiceProvider();

        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _faker = new Faker();
    }
}