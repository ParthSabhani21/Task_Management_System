using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Contract;
using TaskManagement.Core.Domain.RequestModel;

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
    public async Task<IActionResult> LoginUserAsync(long id, string email, string password) 
    {
        var token = _userService.LoginUser(email, password);
       // await _userService.SendEmailAsync(id, email);

        return Ok(token);
    }

    [HttpPost("verifyUser")]
    public async Task<IActionResult> UserVerification(string email, int OTP)
    {
        await _userService.CheckOTP(email, OTP);
        return Ok("User Verified Successfully");
    }

}
