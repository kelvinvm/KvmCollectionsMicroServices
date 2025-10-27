using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using KvmCollection.MediatR.Requests;
using MediatR;

namespace KvmCollection.ImageAi;

public class WebImageManager : IWebImageManager
{
    private readonly IDataRepository<WebImage> _webImageRepository;
    private IMediator _mediator;

    public WebImageManager(IDataRepository<WebImage> webImageRepository, IMediator mediator)
    {
        _webImageRepository = webImageRepository ?? throw new ArgumentNullException(nameof(webImageRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task SaveWebImageAsync(byte[] imageBytes)
    {
        await _mediator.Send(new SaveWebImageRequest(new WebImageInfo(imageBytes)));
    }

    public async Task<string> SaveImageToUrlAsync(byte[] imageBytes, string fileName)
    {
        var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "webImages");
        Directory.CreateDirectory(uploadsDir);

        // Generate unique filename
        var finalFileName = $"{DateTime.Now.Ticks}_{fileName}";
        var filePath = Path.Combine(uploadsDir, finalFileName);

        // Save the file
        using var stream = new FileStream(filePath, FileMode.Create);
        using var memStream = new MemoryStream(imageBytes);
        
        await stream.CopyToAsync(new MemoryStream(imageBytes));


        // Return the URL path
        return $"/images/webImages/{fileName}";
    }
}