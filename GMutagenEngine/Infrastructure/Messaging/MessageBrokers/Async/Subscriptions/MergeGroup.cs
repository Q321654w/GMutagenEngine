using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Handling;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions
{
    public class MergeGroup<TId> : IAsyncDisposable
    {
        private readonly IMessageBroker<TId> _messageBroker;
        private readonly ILogger<MergeGroup<TId>> _logger;
        private readonly TId _target;
        private readonly TId[] _sources;

        private IEnumerable<ISubscription<TId>> _subscriptions;

        public MergeGroup(
            IMessageBroker<TId> messageBroker,
            ILogger<MergeGroup<TId>> logger,
            TId target,
            params TId[] sources)
        {
            _messageBroker = messageBroker;
            _logger = logger;
            _target = target;
            _sources = sources;
        }

        public async Task Init(CancellationToken cancellationToken = default)
        {
            var subscriptions = new ISubscription<TId>[_sources.Length];
            var handler = new ReplicateHandler<TId>(_messageBroker, _logger, _target);

            for (var index = 0; index < _sources.Length; index++)
            {
                var source = _sources[index];
                subscriptions[index] = await _messageBroker.SubscribeForAny(source, handler, cancellationToken);
            }

            _subscriptions = subscriptions;
        }

        public async ValueTask DisposeAsync()
        {
            var tasks = _subscriptions.Select(s => _messageBroker.Unsubscribe(s));
            await Task.WhenAll(tasks);
        }
    }
    
    public class MergeGroup<TId, TData> : IAsyncDisposable
    {
        private readonly IMessageBroker<TId> _messageBroker;
        private readonly ILogger<MergeGroup<TId>> _logger;
        private readonly TId _target;
        private readonly TId[] _sources;

        private IEnumerable<ISubscription<TId>> _subscriptions;

        public MergeGroup(
            IMessageBroker<TId> messageBroker,
            ILogger<MergeGroup<TId>> logger,
            TId target,
            params TId[] sources)
        {
            _messageBroker = messageBroker;
            _logger = logger;
            _target = target;
            _sources = sources;
        }

        public async Task Init(CancellationToken cancellationToken)
        {
            var subscriptions = new ISubscription<TId>[_sources.Length];
            var handler = new ReplicateHandler<TId>(_messageBroker, _logger, _target);

            for (var index = 0; index < _sources.Length; index++)
            {
                var source = _sources[index];
                subscriptions[index] = await _messageBroker.SubscribeForAny(source, handler, cancellationToken);
            }

            _subscriptions = subscriptions;
        }

        public async ValueTask DisposeAsync()
        {
            var tasks = _subscriptions.Select(s => _messageBroker.Unsubscribe(s));
            await Task.WhenAll(tasks);
        }
    }
}