using GMutagenEngine.Infrastructure.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface IMultiStateMachine<TStateId, TTopicId> : IAdvancedIdentifiable<TStateId>
{
    IControlledStateMachine<TStateId, TTopicId>[] StateMachines { get; }
    IMultiStateMachineTransition<TStateId, TTopicId>[] Transitions { get; }

    void Start();
    void Transit(IMultiStateMachineTransition<TStateId, TTopicId> transition);
    void Stop();
}