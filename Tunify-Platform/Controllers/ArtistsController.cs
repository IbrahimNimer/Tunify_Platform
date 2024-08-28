using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class ArtistsController : ControllerBase
{
    private readonly IArtists _artists;
    public ArtistsController(IArtists context)
    {
        _artists = context;
    }

    // GET: api/Artists
    [HttpGet]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<IEnumerable<Artists>>> Getartists()
    {
        return await _artists.GetAllArtist();
    }

    // GET: api/Artists/5
    [HttpGet("{id}")]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<Artists>> GetArtist(int id)
    {
        return await _artists.GetArtistById(id);
    }

    // PUT: api/Artists/5
    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can update
    public async Task<IActionResult> UpdateArtists(int id, Artists artists)
    {
        var updated = await _artists.UpdateArtist(id, artists);
        return Ok(updated);
    }

    // POST: api/Artists
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can create
    public async Task<ActionResult<Artists>> PostArtists(Artists artists)
    {
        return await _artists.CreateArtist(artists);
    }

    // DELETE: api/Artists/5
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can delete
    public async Task DeleteArtists(int id)
    {
        await _artists.DeleteArtist(id);
    }
}
