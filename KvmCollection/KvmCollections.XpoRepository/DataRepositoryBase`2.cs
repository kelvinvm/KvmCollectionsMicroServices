using DevExpress.Xpo;
using KvmCollection.Common.Interfaces;

namespace KvmCollections.XpoRepository;

public abstract class DataRepositoryBase<TDto, TXpo> : IDataRepository<TDto>
    where TDto : IHaveXpoOid
    where TXpo : XPObject
{
    public virtual async Task<TDto> CreateAsync(TDto entity)
    {
        using var uow = new UnitOfWork();
        var xpoObject = MapToXpo(entity, uow);

        await uow.CommitChangesAsync();

        uow.Reload(xpoObject);

        return MapToDto(xpoObject);
    }

    public virtual async Task<TDto> ReadAsync(int key)
    {
        using var uow = new UnitOfWork();
        var xpoObject = await uow.GetObjectByKeyAsync<TXpo>(key);

        if (xpoObject == null)
            throw new InvalidOperationException($"Entity with Oid {key} not found.");

        return MapToDto(xpoObject);
    }

    public virtual async Task<TDto> UpdateAsync(TDto entity)
    {
        using var uow = new UnitOfWork();
        var xpoObject = uow.GetObjectByKey<TXpo>(entity.Oid);

        if (xpoObject == null)
            throw new InvalidOperationException($"Entity with Oid {entity.Oid} not found.");

        MapToXpo(entity, xpoObject);

        await uow.CommitChangesAsync();

        uow.Reload(xpoObject);

        return MapToDto(xpoObject);
    }

    public virtual async Task DeleteAsync(TDto entity)
    {
        using var uow = new UnitOfWork();
        var xpoObject = await uow.GetObjectByKeyAsync<TXpo>(entity.Oid);

        if (xpoObject != null)
        {
            await uow.DeleteAsync(xpoObject);
            await uow.CommitChangesAsync();
        }
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        using var uow = new UnitOfWork();
        var xpoObjects = await uow.Query<TXpo>().ToListAsync();

        return xpoObjects.Select(MapToDto);
    }

    protected abstract TXpo MapToXpo(TDto dto, Session session);
    protected abstract TXpo MapToXpo(TDto dto, TXpo xpo);
    protected abstract TDto MapToDto(TXpo xpo);
}
