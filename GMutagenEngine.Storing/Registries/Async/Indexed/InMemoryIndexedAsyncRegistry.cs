using System.Runtime.CompilerServices;

namespace GMutagenEngine.Storing.Registries.Async.Indexed;

public class InMemoryIndexedAsyncRegistry<TId, TEntity>(Dictionary<TId, TEntity> entities)
    : IIndexedAsyncRegistry<TId, TEntity>
    where TId : notnull
{
    public InMemoryIndexedAsyncRegistry() : this(new Dictionary<TId, TEntity>())
    {
    }

    public Task<TEntity> Add(TId id, TEntity entity, CancellationToken? cancellationToken = null)
    {
        entities[id] = entity;
        return Task.FromResult(entity);
    }

    public Task<TEntity> Get(TId id, CancellationToken? cancellationToken = null)
    {
        if (entities.TryGetValue(id, out var entity))
            return Task.FromResult(entity);

        throw new KeyNotFoundException($"Entity with id {id} not found.");
    }

    public Task<bool> TryGet(TId id, out TEntity entity, CancellationToken? cancellationToken = null)
    {
        return Task.FromResult(entities.TryGetValue(id, out entity));
    }

    public Task<TEntity> Remove(TId id, CancellationToken? cancellationToken = null)
    {
        cancellationToken?.ThrowIfCancellationRequested();

        if (entities.TryGetValue(id, out var entity))
        {
            entities.Remove(id);
            return Task.FromResult(entity);
        }

        throw new KeyNotFoundException($"Entity with id {id} not found.");
    }

    public Task Clear(CancellationToken? cancellationToken = null)
    {
        entities.Clear();
        return Task.CompletedTask;
    }

    public Task<bool> Exists(TId id, CancellationToken? cancellationToken = null)
    {
        return Task.FromResult(entities.ContainsKey(id));
    }

    public async IAsyncEnumerable<TEntity> GetAll([EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in entities.Values)
            yield return entity;
    }

    public Task<TEntity> GetBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        var entity = entities.Values.First(predicate);
        return Task.FromResult(entity);
    }

    public Task<bool> TryGetBy(Func<TEntity, bool> predicate, out TEntity entity, CancellationToken? cancellationToken = null)
    {
        var res = entities.Values.FirstOrDefault(predicate);
        entity = res;
        return Task.FromResult(entity != null);
    }

    public async IAsyncEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate,
        [EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in entities.Values.Where(predicate))
            yield return entity;
    }

    public Task<bool> ExistsBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        var exists = entities.Values.Any(predicate);
        return Task.FromResult(exists);
    }

    public async IAsyncEnumerator<KeyValuePair<TId, TEntity>> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        foreach (var kvp in entities)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return kvp;
            await Task.Yield();
        }
    }
}