using DevExpress.Xpo;
using System;

namespace KvmCollections.XpoRepository.PersistentObjects;

public class Tag : XPObject
{
    private string _description = string.Empty;

    public Tag() : base() { }
    public Tag(Session session) : base(session) { }

    [Size(25)]
    public string Description
    {
        get => _description;
        set => SetPropertyValue(nameof(Description), ref _description, value);
    }

    [Association("ImageTag-Tag")]
    public XPCollection<ImageTag> ImageTags
    {
        get
        {
            return GetCollection<ImageTag>(nameof(ImageTags));
        }
    }
}