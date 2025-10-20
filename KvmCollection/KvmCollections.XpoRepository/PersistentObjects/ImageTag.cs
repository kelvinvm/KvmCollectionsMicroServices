using DevExpress.Xpo;
using System;

namespace KvmCollections.XpoRepository.PersistentObjects;

public class ImageTag : XPObject
{
    private double _confidence;

    public ImageTag() : base() { }
    public ImageTag(Session session) : base(session) { }

    [Association("ImageTag-Image")]
    public Image Image
    {
        get => GetPropertyValue<Image>(nameof(Image));
        set => SetPropertyValue(nameof(Image), value);
    }

    [Association("ImageTag-Tag")]
    public Tag Tag
    {
        get => GetPropertyValue<Tag>(nameof(Tag));
        set => SetPropertyValue(nameof(Tag), value);
    }

    public double Confidence
    {
        get => _confidence;
        set => SetPropertyValue(nameof(Confidence), ref _confidence, value);
    }
}
