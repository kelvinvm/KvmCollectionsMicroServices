using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace KvmCollection.ImageService;

public class ImageProcessor : IImageProcessor
{
    private IDataRepository<CoverArtDto> _coverArtRepository;

    public ImageProcessor(IDataRepository<CoverArtDto> coverArtRepository)
    {
        _coverArtRepository = coverArtRepository ?? throw new ArgumentNullException(nameof(coverArtRepository));
    }

    public async Task<CoverArtDto> CreateCoverArtAsync(string name, byte[] image)
        => await _coverArtRepository.CreateAsync(new CoverArtDto(name, image, DateTime.UtcNow, DateTime.UtcNow));

    public async Task<IEnumerable<CoverArtDto>> GetAvailableCoverArtAsync()
        => await _coverArtRepository.GetAllAsync();
}
