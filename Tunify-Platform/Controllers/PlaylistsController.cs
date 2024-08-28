using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylists _playlists;
    public PlaylistsController(IPlaylists context)
    {
        _playlists = context;
    }

    // GET: api/Playlists
    [HttpGet]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<IEnumerable<Playlists>>> Getplaylists()
    {
        return await _playlists.GetAllPlaylists();
    }

    // GET: api/Playlists/5
    [HttpGet("{id}")]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<Playlists>> GetPlaylists(int id)
    {
        return await _playlists.GetPlaylistByIdAsync(id);
    }

    // PUT: api/Playlists/5
    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can update
    public async Task<IActionResult> PutPlaylists(int id, Playlists playlists)
    {
        var updated = await _playlists.UpdatePlaylist(id, playlists);
        return Ok(updated);
    }

    // POST: api/Playlists
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can create
    public async Task<ActionResult<Playlists>> PostPlaylists(Playlists playlists)
    {
        var created = await _playlists.CreatePlaylistAsync(playlists);
        return Ok(created);
    }

    // DELETE: api/Playlists/5
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can delete
    public async Task DeletePlaylists(int id)
    {
        await _playlists.DeletePlaylistAsync(id);
    }

    // Lab 13 
    [HttpPost("playlists/{playlistId}/songs/{songId}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can add songs to playlists
    public async Task<IActionResult> AddSongToPlaylist(int playlistId, int songId)
    {
        var playlist = await _playlists.AddSongToPlaylist(playlistId, songId);
        return Ok(playlist);
    }
}
