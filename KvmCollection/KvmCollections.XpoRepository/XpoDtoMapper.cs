using DevExpress.Xpo;
using KvmCollection.Common.Dto;
using KvmCollections.XpoRepository.PersistentObjects;

namespace KvmCollections.XpoRepository
{
    public static class XpoDtoMapper
    {
        public static CoverArtDto Map(this CoverArt item)
            => new CoverArtDto(item.Name, item.Image, item.Inserted, item.Updated);

        public static CoverArt Map(this CoverArtDto item, Session xpoSession) 
            => new CoverArt(xpoSession)
            {
                Image = item.Image,
                Name = item.Name,
                Inserted = item.Inserted,
                Updated = item.Updated
            };
    }
}