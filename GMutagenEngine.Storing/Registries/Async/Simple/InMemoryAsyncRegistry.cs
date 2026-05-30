using System.Runtime.CompilerServices;

namespace GMutagenEngine.Storing.Registries.Async.Simple;

public class InMemoryAsyncRegistry<TEntity>(List<TEntity> entities) : IAsyncRegistry<TEntity>
{
    public InMemoryAsyncRegistry() : this(new List<TEntity>())
    {
    }

    public async IAsyncEnumerable<TEntity> GetAll([EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in entities)
            yield return entity;
    }

    public Task<TEntity> GetBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        var result = entities.First(predicate);
        return Task.FromResult(result);
    }

    public Task<bool> TryGetBy(Func<TEntity, bool> predicate, out TEntity entity,
        CancellationToken? cancellationToken = null)
    {
        var result = entities.FirstOrDefault(predicate);
        entity = result;
        return Task.FromResult(result != null);
    }

    public async IAsyncEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate,
        [EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in entities.Where(predicate))
            yield return entity;
    }

    public Task<bool> ExistsBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        bool exists = entities.Any(predicate);
        return Task.FromResult(exists);
    }

    public Task<TEntity> Add(TEntity entity, CancellationToken? cancellationToken = null)
    {
        entities.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> Remove(TEntity entity, CancellationToken? cancellationToken = null)
    {
        var removed = entities.Remove(entity);
        return Task.FromResult(removed);
    }

    public Task Clear(CancellationToken? cancellationToken = null)
    {
        entities.Clear();
        return Task.CompletedTask;
    }

    public async IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return entity;
        }
    }
}