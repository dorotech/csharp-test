using System.Net;
using Application.Common.Interfaces;
using CrossCutting.Models;
using Domain.ValueObjects;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public AuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<ReturnMessage<AuthToken>> SignIn(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return new ReturnMessage<AuthToken>(data: null, HttpStatusCode.Unauthorized);

        var passwordIsValid = await _userManager.CheckPasswordAsync(user, password);

        if (!passwordIsValid)
            return new ReturnMessage<AuthToken>(data: null, HttpStatusCode.Unauthorized);

        var roles = await _userManager.GetRolesAsync(user);

        return await _jwtService.GenerateEncodedToken(user, roles);
    }
}