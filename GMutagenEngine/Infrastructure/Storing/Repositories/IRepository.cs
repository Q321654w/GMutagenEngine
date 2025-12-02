using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.Storing.Repositories
{
    public interface IRepository<in TId, TEntity>
        where TId : notnull
        where TEntity : IIdentifiable<TId>
    {
        Task<TEntity> Create(TEntity entity, CancellationToken? cancellationToken = null);
        Task<TEntity> Read(TId id, CancellationToken? cancellationToken = null);
        Task<TEntity> Update(TId id, TEntity entity, CancellationToken? cancellationToken = null);
        Task<TEntity> Delete(TId id, CancellationToken? cancellationToken = null);

        Task<bool> Exists(TId id, CancellationToken? cancellationToken = null);
        Task<IEnumerable<TEntity>> GetAll(CancellationToken? cancellationToken = null);

        Task<TEntity> ReadBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null);

        Task<IEnumerable<TEntity>> ReadAllBy(Func<TEntity, bool> predicate,
            CancellationToken? cancellationToken = null);

        Task<bool> ExistsBy(Func<TEntity, bool> predicate, CancellationToken? cancellationToken = null);
    }
}