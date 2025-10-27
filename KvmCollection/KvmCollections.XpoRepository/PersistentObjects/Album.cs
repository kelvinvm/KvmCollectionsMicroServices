using DevExpress.Xpo;
using System;

namespace KvmCollections.XpoRepository.PersistentObjects;

public class Album : XPObject
{
    private DateTime _insertedDate;
    private string _subTitle = string.Empty;
    private int _photoCount;
    private string _thumbnail = string.Empty;
    private string _name = string.Empty;

    public Album(Session session) : base(session) 
    {
        InsertedDate = DateTime.UtcNow;
    }
    public Album() : base()
    {
        InsertedDate = DateTime.UtcNow;
    }

    [Size(63)]
    public string Name
    {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    [Size(127)]
    public string Thumbnail
    {
        get => _thumbnail;
        set => SetPropertyValue(nameof(Thumbnail), ref _thumbnail, value);
    }

    public int PhotoCount
    {
        get => _photoCount;
        set => SetPropertyValue(nameof(PhotoCount), ref _photoCount, value);
    }

    public string Subtitle
    {
        get => _subTitle;
        set => SetPropertyValue(nameof(Subtitle), ref _subTitle, value);
    }

    
    public DateTime InsertedDate
    {
        get => _insertedDate;
        set => SetPropertyValue(nameof(InsertedDate), ref _insertedDate, value);
    }

    [Association("Album-Images")]
    public XPCollection<Image> Images
    {
        get
        {
            return GetCollection<Image>(nameof(Images));
        }
    }
}