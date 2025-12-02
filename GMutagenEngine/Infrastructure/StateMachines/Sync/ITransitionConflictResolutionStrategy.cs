namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface ITransitionConflictResolutionStrategy<TStateId, TTopicId>
{
    void Resolve(
        IControlledStateMachine<TStateId, TTopicId> stateMachine,
        ITransition<TStateId, TTopicId> transition,
        IControlledStateMachine<TStateId, TTopicId>[] stateMachines);
}