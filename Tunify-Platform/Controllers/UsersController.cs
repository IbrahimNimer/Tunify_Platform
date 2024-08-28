using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsers _users;

    public UsersController(IUsers context)
    {
        _users = context;
    }

    // GET: api/Users
    [HttpGet]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<IEnumerable<Users>>> Getusers()
    {
        return await _users.GetAllUsers();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<Users>> GetUser(int id)
    {
        return await _users.GetUserById(id);
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can update
    public async Task<IActionResult> PutUsers(int id, Users users)
    {
        var updated = await _users.UpdateUser(id, users);
        return Ok(updated);
    }

    // POST: api/Users
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can create
    public async Task<ActionResult<Users>> PostUsers(Users users)
    {
        return await _users.CreateUser(users);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can delete
    public async Task DeleteUsers(int id)
    {
        await _users.DeleteUser(id);
    }
}

