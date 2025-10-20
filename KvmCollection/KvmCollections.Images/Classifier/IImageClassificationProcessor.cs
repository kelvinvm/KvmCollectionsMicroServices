using KvmCollection.Common.Dto;

namespace KvmCollection.Images.Classifier;

public interface IImageClassificationProcessor
{
    Task<ImageClassification> ProcessAsync(byte[] imageBytes);
}
