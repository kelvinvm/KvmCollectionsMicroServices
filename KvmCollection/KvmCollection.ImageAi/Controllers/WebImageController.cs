using KvmCollection.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace KvmCollection.ImageAi.Controllers;

public class WebImageController : Controller
{
    private readonly IWebImageManager _manager;

    public WebImageController(IWebImageManager manager)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("api/photos/upload-image")]
    public async Task<ActionResult<string>> UploadWebImage([FromForm] IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest("No image file provided.");
        }

        // Validate image file type
        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp" };
        if (!allowedTypes.Contains(image.ContentType.ToLower()))
        {
            return BadRequest("Invalid file type. Please upload an image file.");
        }

        try
        {
            await _manager.SaveImageToUrlAsync(image.CopyToAsync()

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the image.");
        }
    }
}
