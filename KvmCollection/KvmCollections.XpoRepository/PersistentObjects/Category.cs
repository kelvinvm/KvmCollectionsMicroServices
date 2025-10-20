using DevExpress.Xpo;
using System;

namespace KvmCollections.XpoRepository.PersistentObjects;

public class Category : XPObject
{
    private string _description = string.Empty;

    public Category() : base() { }
    public Category(Session session) : base(session) { }

    [Size(63)]
    public string Description
    {
        get => _description;
        set => SetPropertyValue(nameof(Description), ref _description, value);
    }

    [Association("Images-Categories")]
    public XPCollection<Image> Images
    {
        get
        {
            return GetCollection<Image>(nameof(Images));
        }
    }
}
