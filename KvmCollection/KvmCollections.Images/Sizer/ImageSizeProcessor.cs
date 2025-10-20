using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Drawing;

namespace KvmCollection.Images.Sizer;

public class ImageSizeProcessor : IImageSizeProcessor
{
    public byte[] ResizeImage(byte[] imageBytes, int width, int height)
    {
        using Image image = Image.Load(imageBytes);

        image.Mutate(x => x.Resize(width, height, KnownResamplers.Bicubic));

        using var ms = new MemoryStream();
        image.Save(ms, JpegFormat.Instance);

        return ms.ToArray();
    }

    public System.Drawing.Size GetSize(byte[] imageBytes)
    {
        using Image image = Image.Load(imageBytes);
        return new System.Drawing.Size(image.Width, image.Height);
    }
}
