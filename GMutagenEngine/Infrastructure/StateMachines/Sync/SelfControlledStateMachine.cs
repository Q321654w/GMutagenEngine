using GMutagenEngine.Infrastructure.Disposables.Sync.BaseClases;
using GMutagenEngine.Infrastructure.Disposables.Sync.Extensions;
using GMutagenEngine.Infrastructure.Identification.Tagging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public class SelfControlledStateMachine<TStateId, TTopicId, TTransitionEventType>(
    TStateId id,
    HashSet<ITag> tags,
    IState<TStateId> currentState,
    IState<TStateId>[] states,
    ITransition<TStateId, TTopicId>[] transitions)
    : BaseStateMachine<TStateId, TTopicId>(id, tags, currentState, states, transitions), ISelfControlledStateMachine<TStateId, TTopicId>
{
    private CompositeDisposable? _subscriptions;
    
    public void Start()
    {
        if (_subscriptions != null)
            return;

        var subscriptions = Transitions
            .Select(t => t.TriggerChanel.SubscribeAsDisposable<TTopicId, TTransitionEventType>((e) => Transit(t)));
        
        _subscriptions = subscriptions.AsCompositeDisposable();
        CurrentState.Behaviour.Enter();
    }

    public void Stop()
    {
        _subscriptions?.Dispose();
    }

    public void Transit(ITransition<TStateId, TTopicId> transition)
    {
        var previousState = _currentState;
        _currentState = transition.To;

        previousState.Behaviour.Exit();
        _currentState.Behaviour.Enter();
    }
}