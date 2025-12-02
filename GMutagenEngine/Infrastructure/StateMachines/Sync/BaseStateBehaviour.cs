using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public abstract class BaseStateBehaviour<TTopicId>(
    ITopic<TTopicId> beforeEnter,
    ITopic<TTopicId> afterEnter,
    ITopic<TTopicId> beforeExit,
    ITopic<TTopicId> afterExit,
    ILogger logger)
    : IBaseStateBehaviour<TTopicId>
{
    public ITopic<TTopicId> BeforeEnter { get; } = beforeEnter;
    public ITopic<TTopicId> AfterEnter { get; } = afterEnter;

    public ITopic<TTopicId> BeforeExit { get; } = beforeExit;
    public ITopic<TTopicId> AfterExit { get; } = afterExit;

    protected BaseStateBehaviour(ILogger logger) : this(null, null, null, null, logger)
    {
    }

    public void Enter()
    {
        logger.LogInfo(nameof(Enter));
         BeforeEnter.Publish();
        InternalEnter();
        AfterEnter.Publish();
    }

    public void Exit()
    {
        logger.LogInfo(nameof(Exit));
        BeforeExit.Publish();
        InternalExit();
        AfterExit.Publish();
    }

    protected virtual void InternalEnter()
    {
    }

    protected virtual void InternalExit()
    {
    }
}