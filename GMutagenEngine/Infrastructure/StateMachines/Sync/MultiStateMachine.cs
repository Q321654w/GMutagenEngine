using GMutagenEngine.Infrastructure.Disposables.Sync.BaseClases;
using GMutagenEngine.Infrastructure.Disposables.Sync.Extensions;
using GMutagenEngine.Infrastructure.Identification.Identifiable.Realizations;
using GMutagenEngine.Infrastructure.Identification.Tagging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public class MultiStateMachine<TStateId, TTopicId, TTransitionEventType> : AdvancedIdentifiable<TStateId>, IMultiStateMachine<TStateId, TTopicId>
{
    private readonly IControlledStateMachine<TStateId, TTopicId>[] _stateMachines;
    private readonly IMultiStateMachineTransition<TStateId, TTopicId>[] _transitions;
    private readonly ITransitionConflictResolutionStrategy<TStateId, TTopicId> _defaultStrategy;


    private CompositeDisposable? _subscriptions;

    public IControlledStateMachine<TStateId, TTopicId>[] StateMachines => _stateMachines;

    public IMultiStateMachineTransition<TStateId, TTopicId>[] Transitions => _transitions;

    public ITransitionConflictResolutionStrategy<TStateId, TTopicId> DefaultStrategy => _defaultStrategy;

    public MultiStateMachine(TStateId id, HashSet<ITag> tags, IControlledStateMachine<TStateId, TTopicId>[] stateMachines,
        IMultiStateMachineTransition<TStateId, TTopicId>[] transitions)
        : this(id, tags, stateMachines, transitions, new IgnoreResolutionStrategy<TStateId, TTopicId>())
    {
    }

    public MultiStateMachine(TStateId id,
        HashSet<ITag> tags,
        IControlledStateMachine<TStateId, TTopicId>[] stateMachines,
        IMultiStateMachineTransition<TStateId, TTopicId>[] transitions,
        ITransitionConflictResolutionStrategy<TStateId, TTopicId> defaultStrategy) : base(id, tags)
    {
        _stateMachines = stateMachines;
        _transitions = transitions;
        _defaultStrategy = defaultStrategy;
    }

    public void Start()
    {
        if (_subscriptions != null)
            return;

        var subscriptions = Transitions
            .Select(t => t.MainTransition.TriggerChanel.SubscribeAsDisposable<TTopicId, TTransitionEventType>((e) => Transit(t)));
        
        _subscriptions = subscriptions.AsCompositeDisposable();
    }

    public void Stop()
    {
        _subscriptions?.Dispose();
    }

    public void Transit(IMultiStateMachineTransition<TStateId, TTopicId> transition)
    {
        var strategy = transition.Strategy ?? DefaultStrategy;
        strategy.Resolve(transition.StateMachine, transition.MainTransition, StateMachines);
    }
}