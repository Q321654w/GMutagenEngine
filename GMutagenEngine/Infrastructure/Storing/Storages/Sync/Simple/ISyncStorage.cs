using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Simple;

namespace GMutagenEngine.Infrastructure.Storing.Storages.Sync.Simple
{
    public interface ISyncStorage<TEntity> : ISyncRegistry<TEntity>
    {
        TEntity Add(TEntity entity);
        bool Remove(TEntity entity);
        void Clear();
    }
}