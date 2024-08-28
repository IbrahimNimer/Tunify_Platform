using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class SongsController : ControllerBase
{
    private readonly ISongs _songs;
    public SongsController(ISongs context)
    {
        _songs = context;
    }

    // GET: api/Songs
    [HttpGet]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<IEnumerable<Songs>>> Getsongs()
    {
        return await _songs.GetAllSongs();
    }

    // GET: api/Songs/5
    [HttpGet("{id}")]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<Songs>> GetSongs(int id)
    {
        return await _songs.GetSongById(id);
    }

    // PUT: api/Songs/5
    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can update
    public async Task<IActionResult> PutSongs(int id, Songs songs)
    {
        var updated = await _songs.UpdateSong(id, songs);
        return Ok(updated);
    }

    // POST: api/Songs
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can create
    public async Task<ActionResult<Songs>> PostSongs(Songs songs)
    {
        var created = await _songs.CreateSong(songs);
        return Ok(created);
    }

    // DELETE: api/Songs/5
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can delete
    public async Task DeleteSongs(int id)
    {
        await _songs.DeleteSong(id);
    }

    // Lab 13 
    [HttpPost("artists/{artistId}/songs/{songId}")]
    [Authorize(Policy = "RequireAdminRole")] // Only admins can add songs to artists
    public async Task<IActionResult> AddSongToArtist(int artistId, int songId)
    {
        var result = await _songs.AddSongToArtist(artistId, songId);
        return Ok(result);
    }

    [HttpGet("{id}/allSongs/playlists")]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<List<Songs>>> GetSongsForPlaylist(int playlistId)
    {
        var Song = await _songs.GetSongsForPlaylist(playlistId);
        return Ok(Song);
    }

    [HttpGet("{id}/allSongs/artists")]
    [Authorize] // Accessible by authenticated users
    public async Task<ActionResult<List<Songs>>> GetSongsForArtists(int Artistsid)
    {
        var Song = await _songs.GetSongsForArtists(Artistsid);
        return Ok(Song);
    }
}
