using GMutagenEngine.Storing.Registries.Sync.Simple;

namespace GMutagenEngine.Storing.Storages.Sync.Simple;

public interface ISyncStorage<TEntity> : ISyncRegistry<TEntity>, ISyncStorageMark {
    TEntity Add(TEntity entity);
    bool Remove(TEntity entity);
    void Clear();
}