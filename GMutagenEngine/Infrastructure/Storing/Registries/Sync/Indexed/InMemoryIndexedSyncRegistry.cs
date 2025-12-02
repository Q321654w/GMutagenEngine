using System.Collections;

namespace GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed
{
    public class InMemoryIndexedSyncRegistry<TId, TEntity>(Dictionary<TId, TEntity> entities) : IIndexedSyncRegistry<TId, TEntity>
        where TId : notnull
    {
        public InMemoryIndexedSyncRegistry() : this(new Dictionary<TId, TEntity>())
        {
        }

        public TEntity Get(TId id)
        {
            if (!entities.TryGetValue(id, out var entity))
                throw new KeyNotFoundException($"Entity with id '{id}' not found.");
        
            return entity;
        }

        public bool TryGet(TId id, out TEntity entity)
        {
            return entities.TryGetValue(id, out entity);
        }

        public bool Exists(TId id)
        {
            return entities.ContainsKey(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return entities.Values;
        }

        public TEntity GetBy(Func<TEntity, bool> predicate)
        {
            return entities.Values.First(predicate);
        }

        public bool TryGetBy(Func<TEntity, bool> predicate, out TEntity entity)
        {
            var res = entities.Values.FirstOrDefault(predicate);
            entity = res;
            return entity != null;
        }

        public IEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate)
        {
            return entities.Values.Where(predicate);
        }

        public bool ExistsBy(Func<TEntity, bool> predicate)
        {
            return entities.Values.Any(predicate);
        }

        public void Add(TId id, TEntity entity)
        {
            entities[id] = entity;
        }

        public bool Remove(TId id)
        {
            return entities.Remove(id);
        }

        public void Clear()
        {
            entities.Clear();
        }
        public IEnumerator<KeyValuePair<TId, TEntity>> GetEnumerator() => entities.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}