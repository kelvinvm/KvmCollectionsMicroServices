using DevExpress.Xpo;
using System.Linq;

namespace KvmCollections.XpoRepository;

public static class RepositoryExtensions
{
    // For collection synchronization for simple types (.e.g Categories)
    public static void SyncCollection<TXpo>(
        this XPCollection<TXpo> xpoCollection,
        IEnumerable<string> dtoItems,
        Func<TXpo, string> keySelector,
        Func<string, TXpo> createNew)
            where TXpo : XPObject
    {
        var dtoKeys = dtoItems.ToHashSet();
        var existingItems = xpoCollection.ToList();

        // Remove items not in DTO
        foreach (var item in existingItems.Where(x => !dtoKeys.Contains(keySelector(x))))
        {
            xpoCollection.Remove(item);
            item.Delete();
        }

        // Add new items
        var existingKeys = xpoCollection.Select(keySelector).ToHashSet();
        foreach (var dtoItem in dtoItems.Where(d => !existingKeys.Contains(d)))
        {
            xpoCollection.Add(createNew(dtoItem));
        }
    }

    // Generic collection synchronization for complex types (Tags with update logic)
    public static void SyncCollection<TXpo, TDto>(
        this XPCollection<TXpo> xpoCollection,
        IEnumerable<TDto> dtoItems,
        Func<TXpo, string> keySelector,
        Func<TDto, string> dtoKeySelector,
        Func<TDto, TXpo> createNew,
        Action<TXpo, TDto> updateExisting)
            where TXpo : XPObject
    {
        var dtoDict = dtoItems.ToDictionary(dtoKeySelector);
        var existingItems = xpoCollection.ToList();

        // Remove or update existing items
        foreach (var item in existingItems)
        {
            var key = keySelector(item);
            if (!dtoDict.ContainsKey(key))
            {
                xpoCollection.Remove(item);
                item.Delete();
            }
            else
            {
                updateExisting(item, dtoDict[key]);
            }
        }

        // Add new items
        var existingKeys = xpoCollection.Select(keySelector).ToHashSet();
        foreach (var dtoItem in dtoItems.Where(d => !existingKeys.Contains(dtoKeySelector(d))))
        {
            xpoCollection.Add(createNew(dtoItem));
        }
    }
}