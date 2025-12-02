using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Mediators.Async;

namespace GMutagenEngine.Infrastructure.Builders.Async.Common
{
    public class BuilderMediator(IAsyncMediatorHandlerRegistry<IId> registry) : IBuilderIMediator
    {
        public Task<TOut> Send<TIn, TOut>(TIn request, IId? id = null, CancellationToken cancellationToken = default)
            where TIn : IAsyncRequest<TOut>
        {
            var handler = registry.ResolveFunc<TIn, TOut>(id)
                          ?? throw new InvalidOperationException(
                              $"No func<{typeof(TIn).Name}, {typeof(TOut).Name}> registered for {request.GetType().Name}");

            return handler.Handle(request, cancellationToken);
        }

        public Task Send(IId? id = null, CancellationToken cancellationToken = default)
        {
            var handler = registry.ResolveAction(id)
                          ?? throw new InvalidOperationException($"No action handler registered for '{id}'");

            return handler.Handle(cancellationToken);
        }

        public Task Send<TIn>(TIn input, IId? id = null, CancellationToken cancellationToken = default)
    
        {
            var handler = registry.ResolveAction<TIn>(id)
                          ?? throw new InvalidOperationException(
                              $"No action<{typeof(TIn).Name}> handler registered for '{id}'");

            return handler.Handle(input, cancellationToken);
        }

        public Task<TOut> Send<TOut>(IId? id = null, CancellationToken cancellationToken = default)
        {
            var handler = registry.ResolveFunc<TOut>(id)
                          ?? throw new InvalidOperationException(
                              $"No func<{typeof(TOut).Name}> handler registered for '{id}'");

            return handler.Invoke(cancellationToken);
        }
    }
}