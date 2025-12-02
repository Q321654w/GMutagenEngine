using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Handling;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions
{
    public class ReplicationGroup<TId> : IAsyncDisposable
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

        public async Task Init(CancellationToken cancellationToken = default)
        {
            var handler = new ReplicateHandler<TId>(_messageBroker, _logger, _targets);
            _subscription = await _messageBroker.Subscribe(_source, handler, cancellationToken);
        }
        
        public async ValueTask DisposeAsync()
        {
            await _messageBroker.Unsubscribe(_subscription);
        }
    }
    
    public class ReplicationGroup<TId, TEvent> : IAsyncDisposable
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

        public async Task Init(CancellationToken cancellationToken = default)
        {
            var handler = new ReplicateHandler<TId, TEvent>(_messageBroker, _logger, _targets);
            _subscription = await _messageBroker.Subscribe(_source, handler, cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            await _messageBroker.Unsubscribe(_subscription);
        }
    }
}