using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using Microsoft.Identity.Client;
using System.Drawing.Text;

namespace KvmCollection.ImageAi;

public class AlbumManager : IAlbumManager
{
    private readonly IDataRepository<Album> _albumRepository;

    public AlbumManager(IDataRepository<Album> albumRepository)
    {
        _albumRepository = albumRepository ?? throw new ArgumentNullException(nameof(albumRepository));
    }

    public async Task<IEnumerable<Album>> GetAlbumsAsync()
        => await _albumRepository.GetAllAsync();

    public async Task<Album> CreateAlbum(Album album)
        => await _albumRepository.CreateAsync(album);

    public async Task<Album> UpdateAlbum(Album album)  
        => await _albumRepository.UpdateAsync(album);

    public async Task<Album> GetAlbumAsync(int oid)
        => await _albumRepository.ReadAsync(oid);
}
