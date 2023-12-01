using CrossCutting.Models;
using Domain.ValueObjects;

namespace Application.Common.Interfaces;

public interface IAuthenticationService
{
    public Task<ReturnMessage<AuthToken>> SignIn(string email, string password);
}