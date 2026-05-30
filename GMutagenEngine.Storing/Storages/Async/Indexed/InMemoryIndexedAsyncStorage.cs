using System.Runtime.CompilerServices;

namespace GMutagenEngine.Storing.Storages.Async.Indexed;

public class InMemoryIndexedAsyncStorage<TId, TEntity>(Dictionary<TId, TEntity> storage) : IIndexedAsyncStorage<TId, TEntity>
    where TId : notnull
{
    public InMemoryIndexedAsyncStorage() : this(new Dictionary<TId, TEntity>())
    {
    }
    
    public Task<TEntity> Add(TId id, TEntity entity, CancellationToken? cancellationToken = null)
    {
        storage[id] = entity;
        return Task.FromResult(entity);
    }

    public Task<TEntity> Get(TId id, CancellationToken? cancellationToken = null)
    {
        if (storage.TryGetValue(id, out var entity))
            return Task.FromResult(entity);

        throw new KeyNotFoundException($"Entity with id {id} not found.");
    }

    public Task<bool> TryGet(TId id, out TEntity entity, CancellationToken? cancellationToken = null)
    {
        return Task.FromResult(storage.TryGetValue(id, out entity));
    }

    public Task<TEntity> Remove(TId id, CancellationToken? cancellationToken = null)
    {
        cancellationToken?.ThrowIfCancellationRequested();

        if (storage.TryGetValue(id, out var entity))
        {
            storage.Remove(id);
            return Task.FromResult(entity);
        }

        throw new KeyNotFoundException($"Entity with id {id} not found.");
    }

    public Task Clear(CancellationToken? cancellationToken = null)
    {
        storage.Clear();
        return Task.CompletedTask;
    }

    public Task<bool> Exists(TId id, CancellationToken? cancellationToken = null)
    {
        return Task.FromResult(storage.ContainsKey(id));
    }

    public async IAsyncEnumerable<TEntity> GetAll([EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in storage.Values)
            yield return entity;
    }

    public Task<TEntity> GetBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        var entity = storage.Values.First(predicate);
        return Task.FromResult(entity);
    }

    public Task<bool> TryGetBy(Func<TEntity, bool> predicate, out TEntity entity, CancellationToken? cancellationToken = null)
    {
        var res = storage.Values.FirstOrDefault(predicate);
        entity = res;
        return Task.FromResult(entity != null);
    }

    public async IAsyncEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate,
        [EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in storage.Values.Where(predicate))
            yield return entity;
    }

    public Task<bool> ExistsBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        var exists = storage.Values.Any(predicate);
        return Task.FromResult(exists);
    }

    public async IAsyncEnumerator<KeyValuePair<TId, TEntity>> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        foreach (var kvp in storage)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return kvp;
            await Task.Yield();
        }
    }
}