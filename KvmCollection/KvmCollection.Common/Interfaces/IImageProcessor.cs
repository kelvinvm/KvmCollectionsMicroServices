using KvmCollection.Common.Dto;

namespace KvmCollection.Common.Interfaces;

public interface IImageProcessor
{
    Task<IEnumerable<CoverArtDto>> GetAvailableCoverArtAsync();
    Task<CoverArtDto> CreateCoverArtAsync(string name, byte[] image);
}
