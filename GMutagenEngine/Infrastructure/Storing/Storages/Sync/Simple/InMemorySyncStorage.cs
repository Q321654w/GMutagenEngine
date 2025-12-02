namespace GMutagenEngine.Infrastructure.Storing.Storages.Sync.Simple
{
    public class InMemorySyncStorage<TEntity>(List<TEntity> storage) : ISyncStorage<TEntity>
    {
        public InMemorySyncStorage() : this(new List<TEntity>())
        {
        }

        public IEnumerable<TEntity> GetAll()
        {
            return storage;
        }

        public TEntity GetBy(Func<TEntity, bool> predicate)
        {
            var result = storage.First(predicate);
            return result;
        }
    
        public bool TryGetBy(Func<TEntity, bool> predicate, out TEntity entity)
        {
            var res = storage.First(predicate);
            entity = res;
            return res != null;
        }

        public IEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate)
        {
            var results = storage.Where(predicate).ToList();
            return results;
        }

        public bool ExistsBy(Func<TEntity, bool> predicate)
        {
            var exists = storage.Any(predicate);
            return exists;
        }

        public TEntity Add(TEntity entity)
        {
            storage.Add(entity);
            return entity;
        }

        public bool Remove(TEntity entity)
        {
            var removed = storage.Remove(entity);
            return removed;
        }

        public void Clear()
        {
            storage.Clear();
        }

        public IEnumerator<TEntity> GetEnumerator() => storage.GetEnumerator();
    }
}