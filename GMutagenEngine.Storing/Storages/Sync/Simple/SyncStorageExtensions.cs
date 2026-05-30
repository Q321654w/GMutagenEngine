namespace GMutagenEngine.Storing.Storages.Sync.Simple;

public static class SyncStorageExtensions
{
    public static IEnumerable<TEntity> Add<TEntity>(this ISyncStorage<TEntity> storage,
        IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            storage.Add(entity);
        
        return entities;
    }
}