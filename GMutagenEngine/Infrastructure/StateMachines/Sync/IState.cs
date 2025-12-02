
using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface IState<out TId> : IAdvancedIdentifiable<TId>
{
    public IStateBehaviour Behaviour { get; }
}