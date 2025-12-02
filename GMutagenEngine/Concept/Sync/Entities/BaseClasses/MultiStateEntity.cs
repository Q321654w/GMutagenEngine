using GMutagenEngine.Concept.Sync.Entities.Interfaces;
using GMutagenEngine.Concept.Sync.Entities.States.BaseClases;
using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Concept.Sync.Entities.BaseClasses
{
    public class MultiStateEntity<TId>(IEnumerable<Entity<TId>> states, TId id) :
        IEntity<TId>
        where TId : IId
    {
        public TId Id { get; set; } = id;

        private IEnumerable<Entity<TId>> _states = states;
    
        private Entity<TId> _current;

        public MultiStateEntity(IEnumerable<Entity<TId>> states) : this(states, default)
        {
            _current = states.First();
        }

        public TService GetService<TService>() where TService : class, IService
        {
            return _current.GetService<TService>();
        }

        public bool HasService<TService>() where TService : class, IService
        {
            return _current.HasService<TService>();
        }

        public IEnumerable<object> GetAllServices() => _current.GetAllServices();
    }
}