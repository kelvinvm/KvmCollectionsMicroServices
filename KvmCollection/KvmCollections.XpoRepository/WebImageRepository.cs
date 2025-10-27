using DevExpress.Xpo;
using System.Linq;
using Dto = KvmCollection.Common.Dto;
using Xpo = KvmCollections.XpoRepository.PersistentObjects;

namespace KvmCollections.XpoRepository;

public class WebImageRepository : DataRepositoryBase<Dto.WebImage, Xpo.WebImage>
{
    protected override Xpo.WebImage MapToXpo(Dto.WebImage dto, Session session)
    {
        var xpoWebImage = new Xpo.WebImage(session)
        {
            Name = dto.Name,
            Url = dto.Url,
        };
        return xpoWebImage;
    }

    protected override Xpo.WebImage MapToXpo(Dto.WebImage dto, Xpo.WebImage xpo)
    {
        xpo.Name = dto.Name;
        xpo.Url = dto.Url;
        return xpo;
    }

    protected override Dto.WebImage MapToDto(Xpo.WebImage xpo)
    {
        var dto = new Dto.WebImage
        (
            Name: xpo.Name,
            Url: xpo.Url,
            Oid: xpo.Oid
        );
        return dto;
    }
}