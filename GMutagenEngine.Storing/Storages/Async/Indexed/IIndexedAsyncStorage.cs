using GMutagenEngine.Storing.Registries.Async.Indexed;

namespace GMutagenEngine.Storing.Storages.Async.Indexed;

public interface IIndexedAsyncStorage<TId, TEntity> : IIndexedAsyncRegistry<TId, TEntity>, IIndexedAsyncStorageMark {
    Task<TEntity> Add(TId? id, TEntity entity, CancellationToken? cancellationToken = null);
    Task<TEntity> Remove(TId? id, CancellationToken? cancellationToken = null);
    Task Clear(CancellationToken? cancellationToken = null);
}