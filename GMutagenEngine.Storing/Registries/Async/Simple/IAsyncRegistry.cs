namespace GMutagenEngine.Storing.Registries.Async.Simple;

public interface IAsyncRegistry<TEntity> : IAsyncRegistryMark 
{
    IAsyncEnumerable<TEntity> GetAll(CancellationToken? cancellationToken = null);
    Task<TEntity> GetBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null);
    Task<bool> TryGetBy(Func<TEntity, bool> predicate, out TEntity entity, CancellationToken? cancellationToken = null);
    IAsyncEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null);
    Task<bool> ExistsBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null);
}