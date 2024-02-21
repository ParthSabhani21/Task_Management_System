using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;
using TaskManagement.Infra.Domain;

namespace TaskManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("addUser")]
    public async Task<IActionResult> AddUserAsync(UserRequestModel userRequestModel)
    {
        await _userService.AddUserAsync(userRequestModel);
        return Ok($"User {userRequestModel.UserName} Added Successfully");
    }

    [HttpPost("loginUser")]
    public string LoginUser(string username, string password)
    {
        return _userService.LoginUser(username, password);
    }
}
