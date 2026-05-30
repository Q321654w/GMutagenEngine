/*using GMutagenEngine.Infrastructure.GMutagenEngine.Identification.Tagging;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public class StateMachine<TStateId, TTopicId>(TStateId id, 
    HashSet<ITag> tags, 
    IState<TStateId> currentState, 
    IState<TStateId>[] states, ITransition<TStateId, TTopicId>[] transitions)
    : BaseStateMachine<TStateId, TTopicId>(id, tags, currentState, states, transitions), IControlledStateMachine<TStateId, TTopicId>
{
    public void Transit(ITransition<TStateId, TTopicId> transition, bool callExit = true, bool callEnter = true)
    {
        var previousState = _currentState;
        _currentState = transition.To;

        if (callExit)
            previousState.Behaviour.Exit();
        
        if (callEnter)
            _currentState.Behaviour.Enter();
    }
}*/