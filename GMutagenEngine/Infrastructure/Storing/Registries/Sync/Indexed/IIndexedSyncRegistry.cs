using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Simple;

namespace GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed
{
    public interface IIndexedSyncRegistry<TId, TEntity> : IEnumerable<KeyValuePair<TId, TEntity>>, ISyncRegistry<TEntity>
        where TId : notnull
    {
        TEntity Get(TId id);
        bool TryGet(TId id, out TEntity entity);
        bool Exists(TId id);
    }
}