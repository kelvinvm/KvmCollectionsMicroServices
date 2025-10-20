using KvmCollection.MediatR.Requests;
using MediatR;
using System;
using System.Drawing;
using System.Linq;

namespace KvmCollection.Images.Sizer;

internal class SizeRequestHandler : IRequestHandler<SizeRequest, Size>
{
    private readonly IImageSizeProcessor _imageSizeProcessor;

    public SizeRequestHandler(IImageSizeProcessor imageSizeProcessor)
    {
        _imageSizeProcessor = imageSizeProcessor ?? throw new ArgumentNullException(nameof(imageSizeProcessor));
    }

    public Task<Size> Handle(SizeRequest request, CancellationToken cancellationToken)
    {
        var size = _imageSizeProcessor.GetSize(request.ImageBytes);
        return Task.FromResult(size);
    }
}
