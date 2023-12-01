using System.Net;
using Application.Common.Interfaces;
using Application.Features.Users.Commands.SignIn;
using CrossCutting.Models;
using Domain.ValueObjects;
using FakeItEasy;
using Xunit;

namespace Tests.Features.Users.Handlers;

[Trait(nameof(SignInCommand), "Handler")]
public class SignInCommandHandlerTests : TestBase
{
    private readonly SignInCommandHandler _handler;
    private readonly IAuthenticationService _authentication;

    public SignInCommandHandlerTests()
    {
        _authentication = A.Fake<IAuthenticationService>();
        _handler = new SignInCommandHandler(_authentication);
    }

    [Fact]
    public async Task SignInCommandHandler_Exception_ReturnErrorAsync()
    {
        var command = new SignInCommand(_faker.Person.Email, _faker.Person.FirstName);

        A.CallTo(() => _authentication.SignIn(A<string>.Ignored, A<string>.Ignored)).Throws<Exception>();
        
        var result = await _handler.Handle(command, default);
        
        Assert.False(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
    }
    
    [Fact]
    public async Task SignInCommandHandler_IsValid_ReturnSuccessAsync()
    {
        var command = new SignInCommand(_faker.Person.Email, _faker.Person.FirstName);

        var authTokenResponse = new ReturnMessage<AuthToken>(new AuthToken(Guid.NewGuid().ToString(), 7200), HttpStatusCode.OK);
        A.CallTo(() => _authentication.SignIn(A<string>.Ignored, A<string>.Ignored)).Returns(authTokenResponse);
        
        var result = await _handler.Handle(command, default);
        
        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}