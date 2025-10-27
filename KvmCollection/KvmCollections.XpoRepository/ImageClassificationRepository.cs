using Autofac;
using DevExpress.Xpo;
using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using KvmCollections.XpoRepository.PersistentObjects;
using System.Linq;
using Dto = KvmCollection.Common.Dto;
using Xpo = KvmCollections.XpoRepository.PersistentObjects;

namespace KvmCollections.XpoRepository;

public class ImageClassificationRepository : DataRepositoryBase<Dto.ImageClassification, Xpo.Image>
{
    // Create
    protected override Xpo.Image MapToXpo(ImageClassification dto, Session session)
    {
        var xpoImage = new Xpo.Image(session) { ImageData = dto.Image };

        foreach (var category in dto.Categories)
        {
            var xpoCategory = new Xpo.Category(session) { Description = category };
            xpoImage.Category.Add(xpoCategory);
        }

        foreach (var tag in dto.Tags)
        {
            var xpoTag = new Xpo.Tag(session) { Description = tag.Name };
            var xpoImageTag = new Xpo.ImageTag(session)
            {
                Image = xpoImage,
                Tag = xpoTag,
                Confidence = tag.Confidence
            };
            xpoImage.ImageTag.Add(xpoImageTag);
        }
        return xpoImage;
    }

    // Update (with merge logic)
    protected override Xpo.Image MapToXpo(ImageClassification dto, Xpo.Image xpo)
    {
        xpo.ImageData = dto.Image;
        xpo.Description = dto.Description;

        // Update Categories
        xpo.Category.SyncCollection(
            dtoItems: dto.Categories,
            keySelector: cat => cat.Description,
            createNew: desc => new Xpo.Category(xpo.Session) { Description = desc }
        );

        // Update Tags
        xpo.ImageTag.SyncCollection(
            dtoItems: dto.Tags,
            keySelector: imageTag => imageTag.Tag.Description,
            dtoKeySelector: tag => tag.Name,
            createNew: tag => new Xpo.ImageTag(xpo.Session)
            {
                Image = xpo,
                Tag = new Xpo.Tag(xpo.Session) { Description = tag.Name },
                Confidence = tag.Confidence
            },
            updateExisting: (imageTag, tag) => imageTag.Confidence = tag.Confidence
        );

        return xpo;
    }

    // Map output
    protected override ImageClassification MapToDto(Xpo.Image xpo)
    {
        var dto = new Dto.ImageClassification
        (
            Image: xpo.ImageData,
            Description: xpo.Description,
            Categories: xpo.Category.Select(c => c.Description).ToList(),
            Tags: xpo.ImageTag.Select(t => new TagInfo(t.Tag.Description, t.Confidence, t.Oid)).ToList(),
            AnalyzedSize: System.Drawing.Size.Empty,
            OriginalSize: System.Drawing.Size.Empty,
            Oid: xpo.Oid
        );

        return dto;
    }
}
