using GMutagenEngine.Concept.Sync.Entities.Interfaces;
using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Concept.Sync.Entities.States.BaseClases
{
    public class Entity<TId>(Dictionary<Type, object> services, TId id) :
        IEntity<TId>
        where TId : IId
    {
        public TId Id { get; set; } = id;

        private Dictionary<Type, object> Services = services ?? new Dictionary<Type, object>();

        public Entity() : this(new Dictionary<Type, object>(), default)
        {
        }

        public TService GetService<TService>() where TService : class, IService
        {
            if (Services.TryGetValue(typeof(TService), out var service))
                return service as TService;

            return null;
        }

        public bool HasService<TService>() where TService : class, IService
        {
            return Services.ContainsKey(typeof(TService));
        }

        public IEnumerable<object> GetAllServices() => Services.Values;
    }
}