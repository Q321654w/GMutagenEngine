using GMutagenEngine.Infrastructure.Storing.Registries.Async.Simple;

namespace GMutagenEngine.Infrastructure.Storing.Registries.Async.Indexed
{
    public interface IIndexedAsyncRegistry<TId, TEntity> : IAsyncEnumerable<KeyValuePair<TId, TEntity>>, IAsyncRegistry<TEntity>
        where TId : notnull
    {
        Task<TEntity> Get(TId id, CancellationToken? cancellationToken = null);
        Task<bool> TryGet(TId id, out TEntity entity, CancellationToken? cancellationToken = null);
        Task<bool> Exists(TId id, CancellationToken? cancellationToken = null);
    }
}