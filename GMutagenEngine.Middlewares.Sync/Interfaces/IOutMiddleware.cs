using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Sync.Interfaces;

public interface IOutMiddleware<TOut> : IOutMiddleware, IOutMiddlewareMark {
    TOut Invoke(Func<TOut> next);
}