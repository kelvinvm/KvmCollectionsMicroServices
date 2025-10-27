using KvmCollection.Common.Dto;

namespace KvmCollection.ImageAi;

public interface IAlbumManager
{
    Task<Album> GetAlbumAsync(int oid);
    Task<Album> UpdateAlbum(Album album);
    Task<Album> CreateAlbum(Album album);
    Task<IEnumerable<Album>> GetAlbumsAsync();
}
