using DevExpress.DashboardWeb.Native;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using KvmCollection.Common.Dto;
using System;

namespace KvmCollections.XpoRepository.PersistentObjects;

public class CoverArt : XPObject
{
    private byte[] _image = [];
    private string _name = string.Empty;
    private DateTime _inserted;
    private DateTime _updated;

    [Indexed(Unique = true)]
    [Size(36)]
    public string Name
    {
        get => _name;
        set => SetPropertyValue(nameof(Name), ref _name, value);
    }

    public byte[] Image
    {
        get => _image;
        set => SetPropertyValue(nameof(Image), ref _image, value);
    }

    public DateTime Updated
    {
        get => _updated;
        set => SetPropertyValue(nameof(Updated), ref _updated, value);
    }

    public DateTime Inserted
    {
        get => _inserted;
        set => SetPropertyValue(nameof(Inserted), ref _inserted, value);
    }

    public CoverArt() : base()
    {
    }

    public CoverArt(Session session) : base(session)
    {
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
        Updated = DateTime.UtcNow;
        Inserted = DateTime.UtcNow;
    }
}
