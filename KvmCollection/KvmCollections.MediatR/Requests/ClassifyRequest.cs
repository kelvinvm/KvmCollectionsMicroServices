using KvmCollection.Common.Dto;
using MediatR;
using System;
using System.Linq;

namespace KvmCollection.MediatR.Requests;

public record ClassifyRequest(byte[] ImageBytes) : IRequest<ImageClassification>;