using KvmCollection.Common.Dto;
using KvmCollection.MediatR.Requests;
using MediatR;
using System;
using System.Linq;

namespace KvmCollection.Images.Sizer;

internal class ResizeRequestHandler : IRequestHandler<ResizeRequest, Image>
{
    private readonly IImageSizeProcessor _imageSizeProcessor;

    public ResizeRequestHandler(IImageSizeProcessor imageSizeProcessor)
    {
        _imageSizeProcessor = imageSizeProcessor ?? throw new ArgumentNullException(nameof(imageSizeProcessor));
    }

    public Task<Image> Handle(ResizeRequest request, CancellationToken cancellationToken)
    {
        var resizedImage = _imageSizeProcessor.ResizeImage(request.ImageBytes, request.Width, request.Height);
        return Task.FromResult(new Image(resizedImage));
    }
}
