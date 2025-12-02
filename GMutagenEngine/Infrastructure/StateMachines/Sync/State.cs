using GMutagenEngine.Infrastructure.Identification.Identifiable.Realizations;
using GMutagenEngine.Infrastructure.Identification.Tagging;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync
{
    public class State<TId>(TId id, IStateBehaviour behaviour, HashSet<ITag> tags) : AdvancedIdentifiable<TId>(id, tags), IState<TId>
    {
        public IStateBehaviour Behaviour { get; set; } = behaviour;
    }
}