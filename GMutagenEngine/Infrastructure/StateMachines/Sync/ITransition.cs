using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface ITransition<out TStateId, TTopicId>
{
    ITopic<TTopicId> TriggerChanel { get; }
    IState<TStateId> From { get; }
    IState<TStateId> To { get; }
}