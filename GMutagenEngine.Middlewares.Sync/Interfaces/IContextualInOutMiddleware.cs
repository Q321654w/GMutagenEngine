using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Sync.Interfaces;

public interface IInOutMiddleware<TIn, TOut> : IInOutMiddleware, IContextualInOutMiddlewareMark, IInOutMiddlewareMark {
    TOut Invoke(TIn context, Func<TIn, TOut> next);
}
public interface IInOutMiddlewareMark : ISelfMark<IInOutMiddlewareMark> {
}
