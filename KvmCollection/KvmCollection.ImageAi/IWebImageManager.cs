namespace KvmCollection.ImageAi;

public interface IWebImageManager
{
    Task<string> SaveImageToUrlAsync(byte[] imageBytes, string fileName);
    Task SaveWebImageAsync(byte[] imageBytes);
}
