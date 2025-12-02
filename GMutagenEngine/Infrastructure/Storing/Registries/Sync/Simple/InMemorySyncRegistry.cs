namespace GMutagenEngine.Infrastructure.Storing.Registries.Sync.Simple
{
    public class InMemorySyncRegistry<TEntity>(List<TEntity> entities) : ISyncRegistry<TEntity>
    {
        public InMemorySyncRegistry() : this(new List<TEntity>())
        {
        }


        public IEnumerable<TEntity> GetAll()
        {
            return entities;
        }

        public TEntity GetBy(Func<TEntity, bool> predicate)
        {
            return entities.First(predicate);
        }

        public bool TryGetBy(Func<TEntity, bool> predicate, out TEntity entity)
        {
            var res = entities.First(predicate);
            entity = res;
            return res != null;
        }

        public IEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate)
        {
            return entities.Where(predicate);
        }

        public bool ExistsBy(Func<TEntity, bool> predicate)
        {
            return entities.Any(predicate);
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public bool Remove(TEntity entity)
        {
            return entities.Remove(entity);
        }

        public void Clear()
        {
            entities.Clear();
        }

        public IEnumerator<TEntity> GetEnumerator() => entities.GetEnumerator();
    }
}