using KvmCollection.Common.Dto;

namespace KvmCollection.ImageAi;

public interface IImageManager
{
    Task<ImageClassification> ClassifyImageAsync(byte[] imageBytes);
}
