using GMutagenEngine.Storing.Registries.Sync.Simple;

namespace GMutagenEngine.Storing.Registries.Sync.Indexed;

public interface IIndexedSyncRegistry<TId, TEntity> : IEnumerable<KeyValuePair<TId, TEntity>>, ISyncRegistry<TEntity>, IIndexedSyncRegistryMark {
    TEntity Get(TId? id);
    bool TryGet(TId? id, out TEntity entity);
    bool Exists(TId? id);
}