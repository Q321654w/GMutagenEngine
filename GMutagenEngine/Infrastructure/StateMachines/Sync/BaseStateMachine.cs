using GMutagenEngine.Infrastructure.Identification.Identifiable.Realizations;
using GMutagenEngine.Infrastructure.Identification.Tagging;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public class BaseStateMachine<TStateId, TTopicId> : AdvancedIdentifiable<TStateId>, IStateMachine<TStateId, TTopicId>
{
    protected IState<TStateId> _currentState;
    private readonly ITransition<TStateId, TTopicId>[] _transitions;
    private readonly IState<TStateId>[] _states;

    public IState<TStateId> CurrentState => _currentState;
    public ITransition<TStateId, TTopicId>[] Transitions => _transitions;

    public IState<TStateId>[] States => _states;

    public BaseStateMachine(TStateId id, HashSet<ITag> tags, IState<TStateId> currentState, IState<TStateId>[] states,
        ITransition<TStateId, TTopicId>[] transitions) : base(id, tags)
    {
        _currentState = currentState;
        _states = states;
        _transitions = transitions;
    }
}