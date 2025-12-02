using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface IStateGroup<out TId> : IAdvancedIdentifiable<TId>
{
    IState<TId>[] States { get; }
}