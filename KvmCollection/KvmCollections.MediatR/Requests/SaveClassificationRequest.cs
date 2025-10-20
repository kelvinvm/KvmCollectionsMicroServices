using KvmCollection.Common.Dto;
using MediatR;
using System;
using System.Linq;

namespace KvmCollection.MediatR.Requests;

public record SaveClassificationRequest(ImageClassification Classification) : IRequest<ImageClassification>;
