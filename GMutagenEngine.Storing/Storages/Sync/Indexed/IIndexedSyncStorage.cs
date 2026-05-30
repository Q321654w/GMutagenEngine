using GMutagenEngine.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Storing.Storages.Sync.Indexed;

public interface IIndexedSyncStorage<TId, TEntity> : IIndexedSyncRegistry<TId, TEntity>, IIndexedSyncStorageMark {
    TEntity Add(TId? id, TEntity entity);
    TEntity Remove(TId? id);
    void Clear();
}