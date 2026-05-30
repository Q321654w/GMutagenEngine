using GMutagenEngine.Storing.Registries.Async.Simple;

namespace GMutagenEngine.Storing.Registries.Async.Indexed;

public interface IIndexedAsyncRegistry<TId, TEntity> : IAsyncEnumerable<KeyValuePair<TId, TEntity>>, IAsyncRegistry<TEntity>, IIndexedAsyncRegistryMark {
    Task<TEntity> Get(TId? id, CancellationToken? cancellationToken = null);
    Task<bool> TryGet(TId? id, out TEntity entity, CancellationToken? cancellationToken = null);
    Task<bool> Exists(TId? id, CancellationToken? cancellationToken = null);
}