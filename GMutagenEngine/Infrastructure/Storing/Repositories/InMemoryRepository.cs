

using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.Storing.Repositories
{
    public class InMemoryRepository<TId, TEntity>(Func<TEntity, TId> idSelector, Dictionary<TId, TEntity> storage)
        : IRepository<TId, TEntity>
        where TId : notnull
        where TEntity : IIdentifiable<TId>
    {
        public InMemoryRepository(Func<TEntity, TId> idSelector) : this(idSelector, new Dictionary<TId, TEntity>())
        {
        }
    
        private readonly Func<TEntity, TId> _idSelector = idSelector ?? throw new ArgumentNullException(nameof(idSelector));

        public Task<TEntity> Create(TEntity entity, CancellationToken? cancellationToken = null)
        {
            var id = _idSelector(entity);
            if (!storage.TryAdd(id, entity))
                throw new InvalidOperationException($"Entity with id {id} already exists.");

            return Task.FromResult(entity);
        }

        public Task<TEntity> Read(TId id, CancellationToken? cancellationToken = null)
        {
            if (storage.TryGetValue(id, out var entity))
                return Task.FromResult(entity);

            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }

        public Task<TEntity> Update(TId id, TEntity entity, CancellationToken? cancellationToken = null)
        {
            if (!storage.ContainsKey(id))
                throw new KeyNotFoundException($"Entity with id {id} not found.");

            storage[id] = entity;
            return Task.FromResult(entity);
        }

        public Task<TEntity> Delete(TId id, CancellationToken? cancellationToken = null)
        {
            if (storage.Remove(id, out var removed))
                return Task.FromResult(removed);

            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }

        public Task<bool> Exists(TId id, CancellationToken? cancellationToken = null)
        {
            return Task.FromResult(storage.ContainsKey(id));
        }

        public Task<IEnumerable<TEntity>> GetAll(CancellationToken? cancellationToken = null)
        {
            return Task.FromResult<IEnumerable<TEntity>>(storage.Values.ToList());
        }

        public Task<TEntity> ReadBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
        {
            var entity = storage.Values.FirstOrDefault(predicate);
            if (entity is null)
                throw new KeyNotFoundException("Entity not found by predicate.");

            return Task.FromResult(entity);
        }

        public Task<IEnumerable<TEntity>> ReadAllBy(Func<TEntity, bool> predicate,
            CancellationToken? cancellationToken = null)
        {
            var entities = storage.Values.Where(predicate).ToList();
            return Task.FromResult<IEnumerable<TEntity>>(entities);
        }

        public Task<bool> ExistsBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null)
        {
            return Task.FromResult(storage.Values.Any(predicate));
        }
    }
}