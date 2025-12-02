namespace GMutagenEngine.Infrastructure.Storing.Registries.Sync.Simple
{
    public interface ISyncRegistry<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetBy(Func<TEntity, bool> predicate);
        bool TryGetBy(Func<TEntity, bool> predicate, out TEntity entity);
        IEnumerable<TEntity> GetAllBy(Func<TEntity, bool> predicate);
        bool ExistsBy(Func<TEntity, bool> predicate);
    }
}