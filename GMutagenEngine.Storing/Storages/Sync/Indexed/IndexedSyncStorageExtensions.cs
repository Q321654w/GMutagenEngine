namespace GMutagenEngine.Storing.Storages.Sync.Indexed;

public static class IndexedSyncStorageExtensions
{
    public static TEntity Add<TId, TEntity>(this IIndexedSyncStorage<TId, TEntity> storage,
        IEnumerable<TId> ides, TEntity entity)
    {
        foreach (var id in ides)
            storage.Add(id, entity);

        return entity;
    }

    public static TEntity Add<TId, TEntity>(this IIndexedSyncStorage<TId, IEnumerable<TEntity>> storage,
        TId id, TEntity entity)
    {
        if (storage.TryGet(id, out var enumerable))
        {
            var newEnumerable = enumerable.Append(entity);
            storage.Add(id, newEnumerable);
        }
        else
            storage.Add(id, [entity]);
        

        return entity;
    }
    
    public static TEntity Add<TId, TEntity>(this IIndexedSyncStorage<TId, IEnumerable<TEntity>> storage,
        IEnumerable<TId> ides, TEntity entity)
    {
        foreach (var id in ides)
            storage.Add(id, entity);
            
        return entity;
    }
}