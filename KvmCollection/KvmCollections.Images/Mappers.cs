using KvmCollection.Common.Dto;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using SixLabors.ImageSharp;

namespace KvmCollection.Images;

public static class Mappers
{
    public static ImageClassification ToImageClassification(this ImageAnalysis analysis, System.Drawing.Size analyized, System.Drawing.Size original)
    {
        return new ImageClassification(
            Categories: [.. analysis.Categories?.Select(c => c.Name) ?? []],
            Tags: [.. analysis.Tags?.Select(t => new TagInfo(t.Name, t.Confidence, -1)) ?? []],
            AnalyzedSize: analyized,
            OriginalSize: original,
            Image: Array.Empty<byte>(),
            Description: analysis.Description?.Captions?.FirstOrDefault()?.Text ?? string.Empty
        );
    }
}
