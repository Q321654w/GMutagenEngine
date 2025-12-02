using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Handling;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions
{
    public class ReplicationGroup<TId> : IDisposable
    {
        private readonly IMessageBroker<TId> _messageBroker;
        private readonly ILogger<ReplicationGroup<TId>> _logger;
        private readonly TId _source;
        private readonly TId[] _targets;

        private ISubscription<TId> _subscription;

        public ReplicationGroup(
            IMessageBroker<TId> messageBroker,
            ILogger<ReplicationGroup<TId>> logger,
            TId source,
            params TId[] targets)
        {
            _messageBroker = messageBroker;
            _logger = logger;
            _source = source;
            _targets = targets;
        }

        public void Init()
        {
            var handler = new ReplicateHandler<TId>(_messageBroker, _logger, _targets);
            _subscription = _messageBroker.Subscribe(_source, handler);
        }

        public void Dispose()
        {
            _messageBroker.Unsubscribe(_subscription);
        }
    }

    public class ReplicationGroup<TId, TEvent> : IDisposable
    {
        private readonly IMessageBroker<TId> _messageBroker;
        private readonly ILogger<ReplicationGroup<TId>> _logger;
        private readonly TId _source;
        private readonly TId[] _targets;

        private ISubscription<TId> _subscription;

        public ReplicationGroup(
            IMessageBroker<TId> messageBroker,
            ILogger<ReplicationGroup<TId>> logger,
            TId source,
            params TId[] targets)
        {
            _messageBroker = messageBroker;
            _logger = logger;
            _source = source;
            _targets = targets;
        }

        public void Init()
        {
            var handler = new ReplicateHandler<TId, TEvent>(_messageBroker, _logger, _targets);
            _subscription = _messageBroker.Subscribe(_source, handler);
        }

        public void Dispose()
        {
            _messageBroker.Unsubscribe(_subscription);
        }
    }
}