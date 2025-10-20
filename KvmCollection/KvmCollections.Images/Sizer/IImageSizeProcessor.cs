using System.Drawing;

namespace KvmCollection.Images.Sizer;

public interface IImageSizeProcessor
{
    Size GetSize(byte[] imageBytes);
    byte[] ResizeImage(byte[] imageBytes, int width, int height);
}
