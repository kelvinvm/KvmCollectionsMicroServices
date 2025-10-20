
using KvmCollection.Common.Dto;

namespace KvmCollection.Common.Interfaces;

public interface IImageKeyWordProcessor
{
    Task<ImageClassification> ProcessAsync(byte[] imageBytes);
}