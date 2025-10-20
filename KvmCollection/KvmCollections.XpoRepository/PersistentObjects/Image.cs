using DevExpress.Xpo;
using System;

namespace KvmCollections.XpoRepository.PersistentObjects;

public class Image : XPObject
{
    private string _description = string.Empty;
    private byte[] _image = [];

    public Image() : base() { }
    public Image(Session session) : base(session) { }

    public byte[] ImageData
    {
        get => _image;
        set => SetPropertyValue(nameof(ImageData), ref _image, value);
    }
    
    [Size(127)]
    public string Description
    {
        get => _description;
        set => SetPropertyValue(nameof(Description), ref _description, value);
    }

    [Association("Images-Categories")]
    public XPCollection<Category> Category
    {
        get
        {
            return GetCollection<Category>(nameof(Category));
        }
    }

    [Association("Images-ImageTags")]
    public XPCollection<ImageTag> Tag
    {
        get
        {
            return GetCollection<ImageTag>(nameof(Tag));
        }
    }

    [Association("ImageTag-Image")]
    public XPCollection<ImageTag> ImageTag
    {
        get
        {
            return GetCollection<ImageTag>(nameof(ImageTag));
        }
    }
}
