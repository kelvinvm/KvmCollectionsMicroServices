using KvmCollection.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;

namespace KvmCollection.ImageAi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageClassificationController : ControllerBase
{
    private readonly ILogger<ImageClassificationController> _logger;
    private readonly IImageManager _manager;

    public ImageClassificationController(ILogger<ImageClassificationController> logger, IImageManager manager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }

    [HttpPost("extract")]
    public async Task<ActionResult<ImageClassification>> ExtractKeywords([FromForm] IFormFile image)
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
            // Read image bytes
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var classification = await _manager.ClassifyImageAsync(imageBytes);

            return Ok(classification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing image for keyword extraction");
            return StatusCode(500, "An error occurred while processing the image.");
        }
    }
}