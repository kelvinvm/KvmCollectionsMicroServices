using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using KvmCollection.MediatR.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvmCollection.Images.Persister;

internal class SaveClassificationRequestHandler : IRequestHandler<SaveClassificationRequest, ImageClassification>
{
    private readonly IDataRepository<ImageClassification> _repository;

    public SaveClassificationRequestHandler(IDataRepository<ImageClassification> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ImageClassification> Handle(SaveClassificationRequest request, CancellationToken cancellationToken)
    {
        var savedItem = await _repository.CreateAsync(request.Classification);
        return savedItem;
    }
}
    