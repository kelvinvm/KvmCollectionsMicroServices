using DevExpress.Xpo;
using System.Linq;
using Dto = KvmCollection.Common.Dto;
using Xpo = KvmCollections.XpoRepository.PersistentObjects;

namespace KvmCollections.XpoRepository;

public class AlbumRepository : DataRepositoryBase<Dto.Album, Xpo.Album>
{
    protected override Xpo.Album MapToXpo(Dto.Album dto, Session session)
    {
        var xpoAlbum = new Xpo.Album(session)
        {
            Name = dto.Name,
            Thumbnail = dto.ThumbnailUrl,
            PhotoCount = dto.PhotoCount,
            Subtitle = dto.Subtitle
        };
        return xpoAlbum;
    }
    protected override Dto.Album MapToDto(Xpo.Album xpo)
    {
        var dto = new Dto.Album
        (
            Oid: xpo.Oid,
            Name: xpo.Name,
            ThumbnailUrl: xpo.Thumbnail,
            PhotoCount: xpo.PhotoCount,
            Subtitle: xpo.Subtitle
        );
        return dto;
    }
    protected override Xpo.Album MapToXpo(Dto.Album dto, Xpo.Album xpo)
    {
        xpo.Name = dto.Name;
        xpo.Thumbnail = dto.ThumbnailUrl;
        xpo.PhotoCount = dto.PhotoCount;
        xpo.Subtitle = dto.Subtitle;
        return xpo;
    }
}
