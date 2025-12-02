using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Concept.Sync.Entities.Interfaces
{
    public interface IEntity
    {
        TService GetService<TService>() where TService : class, IService;
        bool HasService<TService>() where TService : class, IService;
        IEnumerable<object> GetAllServices();
    }

    public interface IEntity<out TId> : IEntity, IIdentifiable<TId> where TId : IId
    {
    }
}