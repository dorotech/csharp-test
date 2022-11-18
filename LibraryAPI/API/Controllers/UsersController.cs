using AutoMapper;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Services;
using LibraryApi.Extensions;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUsersService usersService)
        {
            _mapper = mapper;
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserModel, User>(model);
            var result = await _usersService.SaveAsync(user, model.Password);

            if (!result.Success)
                return BadRequest(result.Message);

            var userModel = _mapper.Map<User, UserModel>(result.User);
            return Ok(userModel);
        }

        [HttpPost("/api/[controller]/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _usersService.LoginAsync(model);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Token);
        }
    }
}