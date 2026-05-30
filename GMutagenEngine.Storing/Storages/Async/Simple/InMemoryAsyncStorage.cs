using System.Runtime.CompilerServices;

namespace GMutagenEngine.Storing.Storages.Async.Simple;

public class InMemoryAsyncStorage<TEntity>(List<TEntity> storage) : IAsyncStorage<TEntity>
{
    public InMemoryAsyncStorage() : this(new List<TEntity>())
    {
    }


    public async IAsyncEnumerable<TEntity> GetAll([EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in storage)
            yield return entity;
    }


    public Task<TEntity> GetBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        var result = storage.First(predicate);
        return Task.FromResult(result);
    }

    public Task<bool> TryGetBy(Func<TEntity, bool> predicate, out TEntity entity,
        CancellationToken? cancellationToken = null)
    {
        var result = storage.FirstOrDefault(predicate);
        entity = result;
        return Task.FromResult(result != null);
    }

    public async IAsyncEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate,
        [EnumeratorCancellation] CancellationToken? cancellationToken = null)
    {
        foreach (var entity in storage.Where(predicate))
            yield return entity;
    }

    public Task<bool> ExistsBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
    {
        bool exists = storage.Any(predicate);
        return Task.FromResult(exists);
    }

    public Task<TEntity> Add(TEntity entity, CancellationToken? cancellationToken = null)
    {
        storage.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> Remove(TEntity entity, CancellationToken? cancellationToken = null)
    {
        var removed = storage.Remove(entity);
        return Task.FromResult(removed);
    }

    public Task Clear(CancellationToken? cancellationToken = null)
    {
        storage.Clear();
        return Task.CompletedTask;
    }

    public async IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        foreach (var entity in storage)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return entity;
            await Task.Yield();
        }
    }
}