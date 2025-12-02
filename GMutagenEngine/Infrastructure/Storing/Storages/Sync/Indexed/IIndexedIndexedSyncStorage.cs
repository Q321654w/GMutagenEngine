using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed
{
    public interface IIndexedIndexedSyncStorage<TId, TEntity> : IIndexedSyncRegistry<TId, TEntity>
        where TId : notnull
    {
        TEntity Add(TId id, TEntity entity);
        TEntity Remove(TId id);
        void Clear();
    }
}