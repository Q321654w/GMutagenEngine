using System.Collections;

namespace GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed
{
    public class InMemoryIndexedSyncStorage<TId, TEntity>(Dictionary<TId, TEntity> storage)
        : IIndexedIndexedSyncStorage<TId, TEntity>
        where TId : notnull
    {
        protected readonly Dictionary<TId, TEntity> Storage = storage;

        public InMemoryIndexedSyncStorage() : this(new Dictionary<TId, TEntity>())
        {
        }

        public TEntity Add(TId id, TEntity entity)
        {
            Storage[id] = entity;
            return entity;
        }

        public TEntity Get(TId id)
        {
            if (Storage.TryGetValue(id, out var entity))
                return entity;

            throw new KeyNotFoundException($"Entity with id '{id}' not found.");
        }

        public bool TryGet(TId id, out TEntity entity)
        {
            return Storage.TryGetValue(id, out entity);
        }

        public TEntity Remove(TId id)
        {
            if (Storage.TryGetValue(id, out var entity))
            {
                Storage.Remove(id);
                return entity;
            }

            throw new KeyNotFoundException($"Entity with id '{id}' not found.");
        }

        public void Clear()
        {
            Storage.Clear();
        }

        public bool Exists(TId id) => Storage.ContainsKey(id);

        public IEnumerable<TEntity> GetAll() => Storage.Values.ToList();

        public TEntity GetBy(Func<TEntity, bool> predicate)
        {
            return Storage.Values.First(predicate);
        }

        public bool TryGetBy(Func<TEntity, bool> predicate, out TEntity entity)
        {
            var res = Storage.Values.FirstOrDefault(predicate);
            entity = res;
            return entity != null;
        }

        public IEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate) =>
            Storage.Values.Where(predicate).ToList();

        public bool ExistsBy(Func<TEntity, bool> predicate) =>
            Storage.Values.Any(predicate);
        public IEnumerator<KeyValuePair<TId, TEntity>> GetEnumerator() => Storage.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
