namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public class DefaultMultiResolutionStrategy<TStateId, TTopicId>
(Dictionary<IControlledStateMachine<TStateId, TTopicId>,
    ITransition<TStateId, TTopicId>> transitions)
    : ITransitionConflictResolutionStrategy<TStateId, TTopicId>
{
    public void Resolve(
        IControlledStateMachine<TStateId, TTopicId> mainMachine,
        ITransition<TStateId, TTopicId> mainTransition,
        IControlledStateMachine<TStateId, TTopicId>[] stateMachines
    )
    {
        var previousStates = new Dictionary<IControlledStateMachine<TStateId, TTopicId>, IState<TStateId>>();
        var newStates = new Dictionary<IControlledStateMachine<TStateId, TTopicId>, IState<TStateId>>();

        foreach (var sm in stateMachines)
        {
            previousStates[sm] = sm.CurrentState;

            if (transitions.TryGetValue(sm, out var t))
                newStates[sm] = t.To;
            else
                newStates[sm] = sm.CurrentState;
        }
        
        foreach (var sm in stateMachines)
        {
            var prev = previousStates[sm];
            var next = newStates[sm];
            
            if (!ReferenceEquals(prev, next))
                prev.Behaviour.Exit();
        }
        
        foreach (var sm in stateMachines)
        {
            var prev = previousStates[sm];
            var next = newStates[sm];
            
            if (!ReferenceEquals(prev, next))
                sm.Transit(transitions[sm], callExit: false, callEnter: false);
        }
        
        foreach (var sm in stateMachines)
        {
            var prev = previousStates[sm];
            var next = newStates[sm];
            
            if (!ReferenceEquals(prev, next))
                next.Behaviour.Enter();
        }
        
        if (mainMachine != null && mainTransition != null)
        {
            mainMachine.Transit(mainTransition);
        }
    }
}