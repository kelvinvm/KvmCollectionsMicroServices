using KvmCollection.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KvmCollection.ImageAi.Controllers;

public class AlbumController : Controller
{
    private IAlbumManager _albumManager;
    
    public AlbumController(IAlbumManager albumManager)
    {
        _albumManager = albumManager ?? throw new ArgumentNullException(nameof(albumManager));
    }

    [HttpGet]
    [Route("api/albums")]
    public async Task<IActionResult> Index()
    {
        var albums = await _albumManager.GetAlbumsAsync();
        return Ok(albums);
    }

    [HttpGet]
    [Route("api/albums/{oid}")]
    public async Task<IActionResult> GetAlbum(int oid)
    {
        var album = await _albumManager.GetAlbumAsync(oid);
        return Ok(album);
    }

    [HttpPost]
    [Route("api/albums")]
    public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album album)
    {
        if (album == null)
        {
            return BadRequest("Album cannot be null");
        }

        try
        {
            var createdAlbum = await _albumManager.CreateAlbum(album);
            return Ok(album);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("api/albums/{oid}")]
    public async Task<ActionResult<Album>> UpdateAlbum(int oid, [FromBody] Album album)
    {
        var albumWithId = album with { Oid = oid };
        if (album == null || oid != albumWithId.Oid)
        {
            return BadRequest("Invalid album data");
        }
        try
        {
            var updatedAlbum = await _albumManager.UpdateAlbum(albumWithId);
            return Ok(updatedAlbum);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("api/albums/upload-thumbnail")]
    public async Task<IActionResult> UploadThumbnail(IFormFile thumbnail)
    {
        if (thumbnail == null || thumbnail.Length == 0)
            return BadRequest("No file uploaded");

        // Define the upload directory
        var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "albums");

        // Create directory if it doesn't exist
        Directory.CreateDirectory(uploadsDir);

        // Generate unique filename
        var fileName = $"{DateTime.Now.Ticks}_{thumbnail.FileName}";
        var filePath = Path.Combine(uploadsDir, fileName);

        // Save the file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await thumbnail.CopyToAsync(stream);
        }

        // Return the URL path
        var url = $"/images/albums/{fileName}";
        return Ok(new { url });
    }
}
