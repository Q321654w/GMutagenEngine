using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Mediators.Sync;

namespace GMutagenEngine.Infrastructure.Builders.Sync.Common
{
    public class BuilderMediator(ISyncMediatorHandlerRegistry<IId> registry) : IBuilderIMediator
    {
        public TOut Send<TIn, TOut>(TIn request, IId? id = null)
            where TIn : ISyncRequest<TOut>
        {
            var handler = registry.ResolveFunc<TIn, TOut>(id)
                          ?? throw new InvalidOperationException(
                              $"No func<{typeof(TIn).Name}, {typeof(TOut).Name}> registered for {request.GetType().Name}");

            return handler.Handle(request);
        }

        public void Send(IId? id = null)
        {
            var handler = registry.ResolveAction(id)
                          ?? throw new InvalidOperationException($"No action handler registered for '{id}'");

            handler.Handle();
        }

        public void Send<TIn>(TIn input, IId? id = null)
    
        {
            var handler = registry.ResolveAction<TIn>(id)
                          ?? throw new InvalidOperationException(
                              $"No action<{typeof(TIn).Name}> handler registered for '{id}'");

            handler.Handle(input);
        }

        public TOut Send<TOut>(IId? id = null)
        {
            var handler = registry.ResolveFunc<TOut>(id)
                          ?? throw new InvalidOperationException(
                              $"No func<{typeof(TOut).Name}> handler registered for '{id}'");

            return handler.Handle();
        }
    }
}