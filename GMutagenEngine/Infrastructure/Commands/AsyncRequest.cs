using GMutagenEngine.Concept.Sync.Entities.Interfaces;
using GMutagenEngine.Infrastructure.Mediators.Async;

namespace GMutagenEngine.Infrastructure.Commands
{
    public abstract record AsyncRequest<TResponse>(IEntity Sender, IEntity Target) : IAsyncRequest<TResponse>
    {
        public IEntity Sender { get; } = Sender;
        public IEntity Target { get; } = Target;
    }
}