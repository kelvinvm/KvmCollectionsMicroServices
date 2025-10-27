using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using KvmCollection.MediatR.Requests;
using MediatR;
using System;
using System.Linq;

namespace KvmCollection.Images.Persister;


internal class SaveWebImageRequestHandler : IRequestHandler<SaveWebImageRequest>
{
    private readonly IDataRepository<WebImage> _repository;
    private readonly IMediator _mediator;

    public SaveWebImageRequestHandler(IDataRepository<WebImage> repository, IMediator mediator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Handle(SaveWebImageRequest request, CancellationToken cancellationToken)
    {
        await ClassifyImageAsync(request.WebImage.Image);
    }

    public async Task<ImageClassification> ClassifyImageAsync(byte[] imageBytes)
    {
        int maxSidePixels = 1600;
        byte[] resizedImage = new byte[imageBytes.Length];
        Array.Copy(imageBytes, resizedImage, imageBytes.Length);

        while (resizedImage.Length > 4000000)
        {
            if (maxSidePixels < 800)
                throw new InvalidOperationException("Image is too large to process even after resizing.");

            resizedImage = await Resize(resizedImage, maxSidePixels);
            maxSidePixels -= 200;
        }

        // Process keywords
        var classification = await _mediator.Send(new ClassifyRequest(resizedImage));
        var finalClassification = classification with
        {
            OriginalSize = await _mediator.Send(new SizeRequest(imageBytes))
        };

        return await _mediator.Send(new SaveClassificationRequest(finalClassification));
    }

    private async Task<byte[]> Resize(byte[] imageBytes, int maxSide)
    {
        System.Drawing.Size size = await _mediator.Send(new SizeRequest(imageBytes));

        int targetWidth = 0;
        int targetHeight = 0;

        if (size.Width > size.Height)
            targetWidth = maxSide;
        else
            targetHeight = maxSide;

        // Resize the image (0 in one dimension maintains aspect ratio)
        var resizeRsponse = await _mediator.Send(new ResizeRequest(imageBytes, targetWidth, targetHeight));
        return resizeRsponse.ImageBytes;
    }
}