using KvmCollection.Common.Dto;
using MediatR;
using System;
using System.Drawing;
using System.Linq;

namespace KvmCollection.MediatR.Requests;

public record SizeRequest(byte[] ImageBytes) : IRequest<Size>;
