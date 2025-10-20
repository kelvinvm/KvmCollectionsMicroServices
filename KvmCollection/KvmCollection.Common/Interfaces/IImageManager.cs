
using KvmCollection.Common.Dto;

namespace KvmCollection.Common.Interfaces;

public interface IImageManager
{
    Task<ImageClassification> ClassifyImageAsync(byte[] imageBytes);
}
