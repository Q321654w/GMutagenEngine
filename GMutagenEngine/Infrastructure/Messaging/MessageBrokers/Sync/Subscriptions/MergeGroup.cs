using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Handling;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions
{
    public class MergeGroup<TId> : IDisposable
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

        public void Init()
        {
            var subscriptions = new ISubscription<TId>[_sources.Length];
            var handler = new ReplicateHandler<TId>(_messageBroker, _logger, _target);

            for (var index = 0; index < _sources.Length; index++)
            {
                var source = _sources[index];
                subscriptions[index] = _messageBroker.SubscribeForAny(source, handler);
            }

            _subscriptions = subscriptions;
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
                _messageBroker.Unsubscribe(subscription);
        }
    }
    
    public class MergeGroup<TId, TData> : IDisposable
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

        public void Init()
        {
            var subscriptions = new ISubscription<TId>[_sources.Length];
            var handler = new ReplicateHandler<TId>(_messageBroker, _logger, _target);

            for (var index = 0; index < _sources.Length; index++)
            {
                var source = _sources[index];
                subscriptions[index] = _messageBroker.SubscribeForAny(source, handler);
            }

            _subscriptions = subscriptions;
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
                _messageBroker.Unsubscribe(subscription);
        }
    }
}