using GMutagenEngine.Infrastructure.Storing.Registries.Async.Indexed;

namespace GMutagenEngine.Infrastructure.Storing.Storages.Async.Indexed
{
    public interface IIndexedAsyncStorage<TId, TEntity> : IIndexedAsyncRegistry<TId, TEntity>
        where TId : notnull
    {
        Task<TEntity> Add(TId id, TEntity entity, CancellationToken? cancellationToken = null);
        Task<TEntity> Remove(TId id, CancellationToken? cancellationToken = null);
        Task Clear(CancellationToken? cancellationToken = null);
    }
}