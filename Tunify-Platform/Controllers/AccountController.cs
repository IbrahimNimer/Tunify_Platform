using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models.DTOs;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccounts _userManager;

    public AccountController(IAccounts context)
    {
        _userManager = context;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto registerEmployeeDTO)
    {
        var employee = await _userManager.Register(registerEmployeeDTO, this.ModelState);
        if (ModelState.IsValid)
        {
            return Ok(employee);
        }
        return BadRequest(ModelState);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.LoginUser(loginDto.Username, loginDto.Password);

        if (user == null)
        {
            return Unauthorized();
        }
        return user;
    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Logout([FromBody] string Username)
    {
        if (string.IsNullOrEmpty(Username))
        {
            return BadRequest("UserName cannot be empty");
        }
        var user = await _userManager.LogoutUser(Username);

        if (user == null)
        {
            return Unauthorized();
        }
        return user;
    }

    [HttpGet("Profile")]
    [Authorize] // Accessible by authenticated users
    public async Task<IActionResult> GetUserProfile()
    {
        var username = User.Identity.Name;
        var profile = await _userManager.GetProfile(username);
        return Ok(profile);
    }

    [HttpPost("Profile/Update")]
    [Authorize] // Accessible by authenticated users
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateProfileDto updateProfileDto)
    {
        var username = User.Identity.Name;
        var updatedProfile = await _userManager.UpdateProfile(username, updateProfileDto);

        if (updatedProfile == null)
        {
            return BadRequest("Failed to update profile");
        }
        return Ok(updatedProfile);
    }

}
