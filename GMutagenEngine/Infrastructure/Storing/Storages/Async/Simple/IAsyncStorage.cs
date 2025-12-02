using GMutagenEngine.Infrastructure.Storing.Registries.Async.Simple;

namespace GMutagenEngine.Infrastructure.Storing.Storages.Async.Simple
{
    public interface IAsyncStorage<TEntity> : IAsyncRegistry<TEntity>
    {
        Task<TEntity> Add(TEntity entity, CancellationToken? cancellationToken = null);
        Task<bool> Remove(TEntity entity, CancellationToken? cancellationToken = null);
        Task Clear(CancellationToken? cancellationToken = null);
    }
}