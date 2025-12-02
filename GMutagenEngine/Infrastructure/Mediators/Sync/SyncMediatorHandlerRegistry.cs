using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;
using GMutagenEngine.Infrastructure.Handlers.Sync.Funcs;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.Mediators.Sync
{
    public class SyncMediatorHandlerRegistry : ISyncMediatorHandlerRegistry<IId>
    {
        private readonly Dictionary<DoubleTypedId, object> _handlers = new();

        public void RegisterHandler<TIn, TOut>(ISyncFuncHandler<TIn, TOut> handler, IId? id = null)
            where TIn : ISyncRequest<TOut>
        {
            var key = DoubleTypedId.From(typeof(TIn), typeof(TOut), id);
            _handlers[key] = handler;
        }

        public ISyncFuncHandler<TIn, TOut>? ResolveFunc<TIn, TOut>(IId? id = null)
            where TIn : ISyncRequest<TOut>
        {
            var key = DoubleTypedId.From(typeof(TIn), typeof(TOut), id);
            return _handlers.TryGetValue(key, out var handler)
                ? (ISyncFuncHandler<TIn, TOut>)handler
                : null;
        }

        public void RegisterAction(ISyncActionHandler handler, IId? id = null)
        {
            var key = DoubleTypedId.FromValue(id);
            _handlers[key] = handler;
        }

        public void RegisterAction<TIn>(ISyncActionHandler<TIn> handler, IId? id = null)
        {
            var key = DoubleTypedId.FromFirst(typeof(TIn), id);
            _handlers[key] = handler;
        }

        public void RegisterFunc<TOut>(ISyncFuncHandler<TOut> handler, IId? id = null)
        {
            var key = DoubleTypedId.FromSecond(typeof(TOut), id);
            _handlers[key] = handler;
        }

        public ISyncActionHandler? ResolveAction(IId? id = null)
        {
            var key = DoubleTypedId.FromValue(id);
            return _handlers.TryGetValue(key, out var h) ? (ISyncActionHandler)h : null;
        }

        public ISyncActionHandler<TIn>? ResolveAction<TIn>(IId? id = null)
        {
            var key = DoubleTypedId.FromFirst(typeof(TIn), id);
            return _handlers.TryGetValue(key, out var h) ? (ISyncActionHandler<TIn>)h : null;
        }

        public ISyncFuncHandler<TOut>? ResolveFunc<TOut>(IId? id = null)
        {
            var key = DoubleTypedId.FromSecond(typeof(TOut), id);
            return _handlers.TryGetValue(key, out var h) ? (ISyncFuncHandler<TOut>)h : null;
        }
    }
}