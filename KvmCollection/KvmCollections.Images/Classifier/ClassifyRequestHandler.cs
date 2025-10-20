using KvmCollection.Common.Dto;
using KvmCollection.MediatR.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvmCollection.Images.Classifier;

internal class ClassifyRequestHandler : IRequestHandler<ClassifyRequest, ImageClassification>
{
    private readonly IImageClassificationProcessor _classifier;
    private readonly IMediator _mediator;

    public ClassifyRequestHandler(IImageClassificationProcessor classifier, IMediator mediator)
    {
        _classifier = classifier ?? throw new ArgumentNullException(nameof(classifier));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ImageClassification> Handle(ClassifyRequest request, CancellationToken cancellationToken)
    {
        var classification = await _classifier.ProcessAsync(request.ImageBytes);

        return classification with
        {
            AnalyzedSize = await _mediator.Send(new SizeRequest(request.ImageBytes), cancellationToken),
            Image = request.ImageBytes
        };
    }
}
