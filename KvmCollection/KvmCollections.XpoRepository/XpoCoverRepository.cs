using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;
using KvmCollection.Common.Dto;
using KvmCollection.Common.Interfaces;
using KvmCollections.XpoRepository.PersistentObjects;
using System.Runtime.CompilerServices;

namespace KvmCollections.XpoRepository;

public class XpoCoverRepository : IDataRepository<CoverArtDto>
{
    public async Task<CoverArtDto> CreateAsync(CoverArtDto entity)
    {
        using var uow = new UnitOfWork();
        var xpoObject = entity.Map(uow);
        
        await uow.CommitChangesAsync();

        uow.Reload(xpoObject);

        return xpoObject.Map();
    }

    public async Task<IEnumerable<CoverArtDto>> GetAllAsync()
    {
        using var uow = new UnitOfWork();
        var covers = await uow.Query<CoverArt>().ToListAsync();

        return covers.Select(cov => cov.Map());
    }

    public Task<CoverArtDto> ReadAsync(int key)
    {
        throw new NotImplementedException();
    }

    public Task<CoverArtDto> UpdateAsync(CoverArtDto entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(CoverArtDto entity)
    {
        throw new NotImplementedException();
    }
}
