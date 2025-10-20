using KvmCollection.Common.Dto;
using MediatR;
using System;
using System.Linq;

namespace KvmCollection.MediatR.Requests;

public record ResizeRequest(byte[] ImageBytes, int Width, int Height) : IRequest<Image>;

