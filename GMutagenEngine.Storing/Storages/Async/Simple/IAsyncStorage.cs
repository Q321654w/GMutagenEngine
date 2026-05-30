using GMutagenEngine.Storing.Registries.Async.Simple;

namespace GMutagenEngine.Storing.Storages.Async.Simple;

public interface IAsyncStorage<TEntity> : IAsyncRegistry<TEntity>, IAsyncStorageMark {
    Task<TEntity> Add(TEntity entity, CancellationToken? cancellationToken = null);
    Task<bool> Remove(TEntity entity, CancellationToken? cancellationToken = null);
    Task Clear(CancellationToken? cancellationToken = null);
}