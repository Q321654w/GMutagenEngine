using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.General;

public interface IMiddlewareIdFactory<in TId, out TMiddlewareId> : IMiddlewareIdFactoryMark {
    TMiddlewareId Create(TId? id = default);
    TMiddlewareId CreateIn<TIn>(TIn data, TId? id = default);
    TMiddlewareId CreateOut<TOut>(TId? id = default);
    TMiddlewareId CreateInOut<TIn, TOut>(TIn data, TId? id = default);
}
public interface IMiddlewareIdFactoryMark : ISelfMark<IMiddlewareIdFactoryMark> {
}
