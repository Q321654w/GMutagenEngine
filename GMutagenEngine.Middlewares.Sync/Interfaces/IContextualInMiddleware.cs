using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Sync.Interfaces;

public interface IInMiddleware<TIn> : IInMiddleware, IContextualInMiddlewareMark, IInMiddlewareMark {
    void Invoke(TIn context, Action<TIn> next);
}
public interface IInMiddlewareMark : ISelfMark<IInMiddlewareMark> {
}
