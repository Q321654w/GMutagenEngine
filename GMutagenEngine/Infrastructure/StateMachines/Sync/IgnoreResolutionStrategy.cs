namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public class IgnoreResolutionStrategy<TStateId, TTopicId> : ITransitionConflictResolutionStrategy<TStateId, TTopicId>
{
    public void Resolve(IControlledStateMachine<TStateId, TTopicId> stateMachine, ITransition<TStateId, TTopicId> transition,
        IControlledStateMachine<TStateId, TTopicId>[] stateMachines)
    {
        stateMachine.Transit(transition);
    }
}