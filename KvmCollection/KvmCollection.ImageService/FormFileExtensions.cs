namespace KvmCollection.ImageService;

public static class FormFileExtensions
{
    public static byte[] GetBytes(this IFormFile file)
    {
        using var stream = new MemoryStream();
        file.CopyTo(stream);

        return stream.ToArray();
    }
}
