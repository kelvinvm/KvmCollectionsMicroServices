using KvmCollection.Common.Dto;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using SixLabors.ImageSharp;

namespace KvmCollection.Images.Classifier;

public class ImageClassificationProcessor : IImageClassificationProcessor
{
    private readonly IComputerVisionClient _computerVisionClient;

    public ImageClassificationProcessor(IComputerVisionClient computerVisionClient)
    {
        _computerVisionClient = computerVisionClient ?? throw new ArgumentNullException(nameof(computerVisionClient));
    }

    public async Task<ImageClassification> ProcessAsync(byte[] imageBytes)
    {
        var features = new List<VisualFeatureTypes?>()
        {
            VisualFeatureTypes.Tags,
            VisualFeatureTypes.Description,
            VisualFeatureTypes.Categories,
        };

        try
        {
            ImageAnalysis results = await _computerVisionClient.AnalyzeImageInStreamAsync(new MemoryStream(imageBytes), features);

            // ToDo: Once I get the messaging straightened out, add the real sizes here, or maybe remove them.
            return results.ToImageClassification(System.Drawing.Size.Empty, System.Drawing.Size.Empty);
        }
        catch (Exception ex)
        {
            return new ImageAnalysis().ToImageClassification(System.Drawing.Size.Empty, System.Drawing.Size.Empty); 
        }
    }
}
