using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Async.Interfaces;

public interface IInMiddleware<TIn> : IInMiddleware, IInMiddlewareMark {
    Task Invoke(
        TIn context,
        Func<TIn, CancellationToken, Task> next,
        CancellationToken cancellationToken = default);
}