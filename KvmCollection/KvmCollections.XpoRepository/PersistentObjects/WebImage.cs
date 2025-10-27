using DevExpress.Xpo;
using System;

namespace KvmCollections.XpoRepository.PersistentObjects;

public class WebImage : XPObject
{
    public WebImage(Session session) : base(session)
    {
        InsertedDate = DateTime.UtcNow;
    }

    public WebImage() : base()
    {
        InsertedDate = DateTime.UtcNow;
    }

    private DateTime _insertedDate;
    private string _url = string.Empty;
    private string _name = string.Empty;

    [Size(63)]
    public string Name
    {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    [Size(127)]
    public string Url
    {
        get => _url;
        set => SetPropertyValue(nameof(Url), ref _url, value);
    }

    public DateTime InsertedDate
    {
        get => _insertedDate;
        set => SetPropertyValue(nameof(InsertedDate), ref _insertedDate, value);
    }

    [Association("WebImage-Image")]
    public XPCollection<WebImage> Image
    {
        get
        {
            return GetCollection<WebImage>(nameof(Image));
        }
    }
}