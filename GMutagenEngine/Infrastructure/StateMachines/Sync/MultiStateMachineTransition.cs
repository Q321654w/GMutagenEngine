namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public class MultiStateMachineTransition<TStateId, TTopicId>(
    IControlledStateMachine<TStateId, TTopicId> stateMachine,
    ITransition<TStateId, TTopicId> mainTransition,
    ITransitionConflictResolutionStrategy<TStateId, TTopicId>? strategy)
    : IMultiStateMachineTransition<TStateId, TTopicId>
{
    public IControlledStateMachine<TStateId, TTopicId> StateMachine { get; set; } = stateMachine;
    public ITransition<TStateId, TTopicId> MainTransition { get; } = mainTransition;
    public ITransitionConflictResolutionStrategy<TStateId, TTopicId>? Strategy { get; } = strategy;
}