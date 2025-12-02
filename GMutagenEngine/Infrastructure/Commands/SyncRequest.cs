using GMutagenEngine.Concept.Sync.Entities.Interfaces;
using GMutagenEngine.Infrastructure.Mediators.Sync;

namespace GMutagenEngine.Infrastructure.Commands
{
    public abstract record SyncRequest<TResponse>(IEntity Sender, IEntity Target) : ISyncRequest<TResponse>
    {
        public IEntity Sender { get; } = Sender;
        public IEntity Target { get; } = Target;
    }
}