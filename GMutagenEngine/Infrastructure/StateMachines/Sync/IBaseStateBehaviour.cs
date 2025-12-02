using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface IBaseStateBehaviour<TTopicId> : IStateBehaviour
{
    ITopic<TTopicId> BeforeEnter { get; }
    ITopic<TTopicId> AfterEnter { get; }
    ITopic<TTopicId> BeforeExit { get; }
    ITopic<TTopicId> AfterExit { get; }
}